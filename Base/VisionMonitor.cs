/****************************** Header ******************************\
Class Name: VisionMonitor [singleton]
Summary: Singleton to manage communication and interpretation of 
data reviced from the vision Co-Processor.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using NetworkTables;
using NetworkTables.Tables;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
//TODO: Ryan, finish commenting this file -> thanks.
namespace Base
{
    /// <summary>
    ///     Singleton to manage communication and interpretation of
    ///     data reviced from the vision Co-Processor
    /// </summary>
    public sealed class VisionMonitor
    {
        #region Private Constructors

        /// <summary>
        ///     Default constructor, only called by the class itself
        /// </summary>
        private VisionMonitor()
        {
            ntRelayTable.AddTableListener(new TableActivityListener(), true);
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        ///     Returns the instance of the Vision Monitor
        /// </summary>
        public static VisionMonitor Instance => _lazy.Value;

        #endregion Public Properties

        #region Private Classes

        private class TableActivityListener : ITableListener
        {
            #region Public Methods

            public void ValueChanged(ITable source, string key, object value, NotifyFlags flags)
            {
                if (!key.StartsWith("TARGET")) return;
                _currentTarget = _frameFromByteArray(source.GetRaw(key));
                _updateTarget(_currentTarget);

                if ((_currentTarget.ID != 0) || (_currentTarget.HasTarget == lastDashboardUpdate)) return;
                FrameworkCommunication.Instance.GetDashboardComm().PutBoolean("TARGET", _currentTarget.HasTarget);
                lastDashboardUpdate = _currentTarget.HasTarget;
            }

            #endregion Public Methods

            #region Private Fields

            private static CommunicationFrames.Target _currentTarget;
            private bool lastDashboardUpdate;

            #endregion Private Fields

            #region Private Methods

            private static CommunicationFrames.Target _frameFromByteArray(byte[] arr)
            {
                var target = new CommunicationFrames.Target();
                var size = Marshal.SizeOf(target);
                var ptr = Marshal.AllocHGlobal(size);
                Marshal.Copy(arr, 0, ptr, size);
                target = (CommunicationFrames.Target)Marshal.PtrToStructure(ptr, target.GetType());
                Marshal.FreeHGlobal(ptr);
                return target;
            }


            private static void _updateTarget(CommunicationFrames.Target target)
            {
                for (var i = 0; i < _targets.Count; i++)
                {
                    var o = _targets[i];
                    if ((o != null) && (o.Target.ID == target.ID))
                        _targets.RemoveAt(i);
                }
                var tmp = new CommunicationFrames.TargetContainer();
                tmp.Target = new CommunicationFrames.Target(target);

                _targets.Add(tmp);
            }

            #endregion Private Methods
        }

        #endregion Private Classes

        #region Private Fields

        private static readonly Lazy<VisionMonitor> _lazy =
            new Lazy<VisionMonitor>(() => new VisionMonitor());

        private static readonly List<CommunicationFrames.TargetContainer> _targets = new List<CommunicationFrames.TargetContainer>();
        private readonly NetworkTable ntRelayTable = FrameworkCommunication.Instance.GetVisonRelayComm();

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        ///     Creates a target setting on the Co-Processor
        /// </summary>
        /// <param name="id">ID for the setting</param>
        /// <param name="enabled">If the setting should be enabled</param>
        /// <param name="minRadius">Minimum radius of the target</param>
        /// <param name="maxRadius">Maximum radius of the target</param>
        /// <param name="lowerHue">Lower hue of the target</param>
        /// <param name="lowerSat">Lower saturation of the target</param>
        /// <param name="lowerVal">Lower value of the target</param>
        /// <param name="upperHue">Upper hue of the target</param>
        /// <param name="upperSat">Upper saturation of the target</param>
        /// <param name="upperVal">Upper value of the target</param>
        /// <param name="maxObj">Maximum possible targets to sort through</param>
        public void CreateTargetSetting(int id, bool enabled, int minRadius, int maxRadius, byte lowerHue, byte lowerSat,
            byte lowerVal, byte upperHue, byte upperSat, byte upperVal, int maxObj = 125)
        {
            var frame = new CommunicationFrames.TargetSetting
            {
                ID = id,
                Enabled = enabled,
                MaxObjects = maxObj,
                LowerHue = lowerHue,
                UpperHue = upperHue,
                LowerValue = lowerVal,
                UpperValue = upperVal,
                MinimumRadius = minRadius,
                MaximumRadius = maxRadius,
                LowerSaturation = lowerSat,
                UpperSaturation = upperSat
            };

            var size = Marshal.SizeOf(frame);
            var arr = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(frame, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            ntRelayTable.PutRaw($"CREATE_TARGET_SETTING_{id}", arr);
        }

        //TODO: fix DELETE_TARGET_SETTING to use prefix and rename to DISABLE_TARGET...
        /// <summary>
        ///     Deletes an target setting from the Co-Processors list
        ///     of target to search for
        /// </summary>
        /// <param name="id">the id of the target setting</param>
        public void DeleteFrameSetting(int id)
        {
            ntRelayTable.PutNumber($"DELETE_TARGET_SETTING_{id}", id);
        }

        /// <summary>
        ///     Gets the latest target data from a target id,
        ///     returns null if there is no target data available
        /// </summary>
        /// <param name="id">id of the target setting</param>
        /// <returns></returns>
        public CommunicationFrames.TargetContainer GetLatestTargetData(int id)
        {
            foreach (var target in _targets)
                if ((target != null) && (target.Target.ID == id))
                    return target;
            return null;
        }

        #endregion Public Methods
    }
}