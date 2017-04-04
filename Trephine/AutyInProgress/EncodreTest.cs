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

namespace Trephine.Autonomi
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
                FrameworkCommunication.Instance.SendData(" Left Encoder Value: ", baseCalls.LeftMotor().GetEncoderValue());
                FrameworkCommunication.Instance.SendData(" Right Encoder Value: ", baseCalls.RightMotor().GetEncoderValue());
            }
        }

        #endregion Protected Methods
    }
}