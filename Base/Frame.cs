using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public static class Frame
    {
        public struct Target
        {
            public int X, Y;
            public int Radius;
            public int Width, Height;
            public bool ValidImage, HasTarget;
        }

        public struct FrameSetting
        {
            public int ID;
            public Target Target;
            public int MaximumRadius, MinimumRadius, MaxObjects;
            public byte LowerHue, LowerSaturation, LowerValue;
            public byte UpperHue, UpperSaturation, UpperValue;
        }
    }
}
