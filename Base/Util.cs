using System;
using System.Runtime.InteropServices;

namespace Base
{
    static class Util
    {
        public static T _FrameFromByteArray<T>(byte[] arr, Type type)
        {
            T target;
            var size = Marshal.SizeOf(type);
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(arr, 0, ptr, size);
            target = (T)Marshal.PtrToStructure(ptr, type);
            Marshal.FreeHGlobal(ptr);
            return target;
        }
    }
}
