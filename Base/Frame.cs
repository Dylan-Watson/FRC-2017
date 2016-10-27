/****************************** Header ******************************\
Class Name: Frame
Summary: Defines the frame for the camera vision software used for 
targeting.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//TODO: Ryan, check all of the comments I made in here
namespace Base
{
    /// <summary>
    ///     Class used to define the frame used by the vision targeting.
    /// </summary>
    public static class Frame
    {
        /// <summary>
        ///     Struct used to define the vision target
        /// </summary>
        public struct Target
        {
            /// <summary>
            ///     TODO: Ryan, comment
            /// </summary>
            public int X, Y;

            /// <summary>
            ///     Radius of target
            /// </summary>
            public int Radius;
            
            /// <summary>
            ///     Width and Height of target
            /// </summary>
            public int Width, Height;

            /// <summary>
            ///     TODO: Ryan, comment
            /// </summary>
            public bool ValidImage, HasTarget;
        }

        /// <summary>
        ///     Struct used to establish settings for the Frame
        /// </summary>
        public struct FrameSetting
        {
            /// <summary>
            ///     Unique ID of the frame
            /// </summary>
            public int ID;
            
            /// <summary>
            ///     Target to find: <see cref="Frame.Target"/>
            /// </summary>
            public Target Target;

            /// <summary>
            ///     TODO: Ryan, comment
            /// </summary>
            public int MaximumRadius, MinimumRadius, MaxObjects;

            /// <summary>
            ///     TODO: Ryan, comment
            /// </summary>
            public byte LowerHue, LowerSaturation, LowerValue;

            /// <summary>
            ///     TODO: Ryan, comment
            /// </summary>
            public byte UpperHue, UpperSaturation, UpperValue;
        }
    }
}
