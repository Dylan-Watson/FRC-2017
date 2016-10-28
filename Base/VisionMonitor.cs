using System;
using System.Runtime.InteropServices;
using NetworkTables;
using NetworkTables.Tables;
using System.Collections.Generic;
using System.Linq;

namespace Base
{
    public sealed class VisionMonitor
    {
        private NetworkTable table = FrameworkCommunication.Instance.GetVisonRelayComm();
        public VisionMonitor()
        {
            table.AddTableListener(new TableActivityListener(), true);
        }

        public static VisionMonitor Instance => _lazy.Value;


        private static readonly Lazy<VisionMonitor> _lazy =
            new Lazy<VisionMonitor>(() => new VisionMonitor());


        private static Frame.Target _currentFrame;

        public Frame.Target GetCurrentFrame() => _currentFrame;

        public void CreateFrameSetting(int id, bool enabled, int minRadius, int maxRadius, byte lowerHue, byte lowerSat, byte lowerVal, byte upperHue, byte upperSat, byte upperVal, int maxObj = 125)
        {
            var frame = new Frame.TargetSetting
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
            table.PutRaw($"CREATE_TARGET_SETTING_{id}", arr);
        }

        public void DeleteFrameSetting(int id)
        {
            table.PutNumber("DELETE_TARGET_SETTING", id);
        }

        private class TableActivityListener : ITableListener
        {
            private bool lastDashboardUpdate;

            public void ValueChanged(ITable source, string key, object value, NotifyFlags flags)
            {
                if (key.StartsWith("TARGET"))
                {
                    _currentFrame = frameFromByteArray(source.GetRaw(key));

                    if (_currentFrame.ID == 0 && _currentFrame.HasTarget != lastDashboardUpdate)
                    {
                        FrameworkCommunication.Instance.GetDashboardComm().PutBoolean("TARGET", _currentFrame.HasTarget);
                        //Report.General($"Target: {_currentFrame.HasTarget} - {_currentFrame.ID}");
                        lastDashboardUpdate = _currentFrame.HasTarget;
                    }
                }
            }

            private Frame.Target frameFromByteArray(byte[] arr)
            {
                var target = new Frame.Target();
                var size = Marshal.SizeOf(target);
                var ptr = Marshal.AllocHGlobal(size);
                Marshal.Copy(arr, 0, ptr, size);

                target = (Frame.Target)Marshal.PtrToStructure(ptr, target.GetType());
                Marshal.FreeHGlobal(ptr);

                return target;
            }
        }
    }
}
