using System;
using System.Runtime.InteropServices;
using NetworkTables;
using NetworkTables.Tables;

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


        private static Frame.FrameSetting _currentFrame;

        public Frame.FrameSetting GetCurrentFrame() => _currentFrame;


        public void CreateFrameSetting(int id, int minRadius, int maxRadius, byte lowerHue, byte lowerSat, byte lowerVal, byte upperHue, byte upperSat, byte upperVal, int maxObj = 125)
        {
            var frame = new Frame.FrameSetting
            {
                ID = id,
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
            table.PutRaw("CREATE_FRAME_SETTING", arr);
        }

        public void DeleteFrameSetting(int id)
        {
            table.PutNumber("DELETE_FRAME_SETTING", id);
        }

        private class TableActivityListener : ITableListener
        {
            public void ValueChanged(ITable source, string key, object value, NotifyFlags flags)
            {
                if (key == "FRAME")
                    _currentFrame = frameFromByteArray(source.GetRaw("FRAME"));
            }

            private Frame.FrameSetting frameFromByteArray(byte[] arr)
            {
                var frame = new Frame.FrameSetting();
                var size = Marshal.SizeOf(frame);
                var ptr = Marshal.AllocHGlobal(size);
                Marshal.Copy(arr, 0, ptr, size);

                frame = (Frame.FrameSetting)Marshal.PtrToStructure(ptr, frame.GetType());
                Marshal.FreeHGlobal(ptr);

                return frame;
            }
        }
    }
}
