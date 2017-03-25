/****************************** Header ******************************\
Class Name: DriveStrait
Summary: Practice autonomous made to drive straight
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using Base;
using WPILib;

namespace Trephine
{
    internal class EncodreTest : Autonomous
    {
        #region Protected Methods

        protected override void main()
        {
            baseCalls.LeftMotor().ResetEncoder();
            baseCalls.RightMotor().ResetEncoder();

            while (true)
            {
                baseCalls.LeftMotor().GetEncoderValue();
                baseCalls.RightMotor().GetEncoderValue();
            }
        }

        #endregion Protected Methods
    }
}