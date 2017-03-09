﻿using Base;

namespace Trephine.Autonomi
{
    public class NavXTest : Autonomous
    {
        #region Protected Methods

        protected override void main()
        {
            while (true)
            {
                FrameworkCommunication.Instance.SendData("-X", NavX.Instance.GetAngle());
            }
        }

        #endregion Protected Methods
    }
}
