﻿/****************************** Header ******************************\
Class Name: CommunicationFrames
Summary: Defines the frame for the camera vision software used for
targeting.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using WPILib;

namespace Base
{
    /// <summary>
    ///     Static Class to hold communication frame structures for communication
    ///     between RIO and Co-Processors
    /// </summary>
    public static class CommunicationFrames
    {
        #region Public Classes

        /// <summary>
        ///     Class to define a communication frame time stamp
        ///     based off of the current match time
        /// </summary>
        public class FrameTimeStamp
        {
            #region Public Fields

            /// <summary>
            ///     Time in seconds
            /// </summary>
            public readonly double TimeStamp;

            #endregion Public Fields

            #region Public Constructors

            /// <summary>
            ///     Default constructor
            /// </summary>
            public FrameTimeStamp()
            {
                TimeStamp = DriverStation.Instance.GetMatchTime();
            }

            #endregion Public Constructors
        }

        #endregion Public Classes

        #region Public Structs

        /// <summary>
        ///     Struct used to define a vision target
        /// </summary>
        //[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
        public struct Target
        {
            #region Public Fields

            /// <summary>
            ///     The ID of the originating TargetSetting
            /// </summary>
            //[FieldOffset(0)]
            public int ID;

            /// <summary>
            ///     The X and Y placment of the target's centre
            /// </summary>
            //[FieldOffset(1)]
            public int X, Y;

            /// <summary>
            ///     Radius of target
            /// </summary>
            //[FieldOffset(2)]
            public int Radius;

            /// <summary>
            ///     Width and Height of target
            /// </summary>
            //[FieldOffset(3)]
            public int Width, Height;

            /// <summary>
            ///     Booleans to define if the image is valid and if
            ///     the target is in view. (ValidImage would only be false
            ///     if something went wrong with grabbing the image from
            ///     the camera in OpenCV)
            /// </summary>
            //[FieldOffset(4)]
            public bool ValidImage, HasTarget;

            #endregion Public Fields

            #region Public Constructors

            /// <summary>
            ///     Creates a new target from another target
            /// </summary>
            /// <param name="t">target to copy</param>
            public Target(Target t)
            {
                ID = t.ID;
                X = t.X;
                Y = t.Y;
                Radius = t.Radius;
                Width = t.Width;
                Height = t.Height;
                ValidImage = t.ValidImage;
                HasTarget = t.HasTarget;
            }

            #endregion Public Constructors
        }

        /// <summary>
        ///     Class to contain the Target and the TimeStamp
        /// </summary>
        public class TargetContainer
        {
            /// <summary>
            ///     The target to be contained
            /// </summary>
            public Target Target;

            /// <summary>
            ///     Time stamp of the communication
            /// </summary>
            public FrameTimeStamp TimeStamp;
        }

        /// <summary>
        ///     Struct used to define search parameters for a target
        /// </summary>
        public struct TargetSetting
        {
            #region Public Fields

            /// <summary>
            ///     Defines if the target should be searched for
            /// </summary>
            public bool Enabled;

            /// <summary>
            ///     Unique ID of the target
            /// </summary>
            public int ID;

            /// <summary>
            ///     Defines the HSV lower and upper bound search parameters
            /// </summary>
            public byte LowerHue, UpperHue, LowerValue, UpperValue, LowerSaturation, UpperSaturation, Red, Green, Blue;

            /// <summary>
            ///     Defines the search parameters for an object
            /// </summary>
            public int MinimumRadius, MaximumRadius, MaxObjects;

            #endregion Public Fields
        }

        #endregion Public Structs
    }
}