/****************************** Header ******************************\
Class Name: Sensing inherits ControlLoop
Summary: Loop that handles all sensor activities.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using Base;
using System;
using Base.Components;
using WPILib;

namespace Tourniquet
{
    /// <summary>
    /// Class to handle sensors on the robot, this is a ControlLoop, see ControlLoop in Base
    /// </summary>
    public class Sensing : ControlLoop
    {
        #region Protected Methods

        private Config config;
        private readonly AnalogInputItem preassurePad;
        private readonly CanTalonItem shooter;

        public Sensing(Config config)
        {
            this.config = config;
            preassurePad = (AnalogInputItem)config.ActiveCollection.Get(new CommonName("p_pad"));
            shooter = (CanTalonItem) config.ActiveCollection.Get(new CommonName("shooter_1"));
        }


        /// <summary>
        /// Instruction to execute within the loop
        /// </summary>
        protected override void main()
        {
            var control = ControlCollection.Instance.GetOperatorControl("intakeIn");
            if (preassurePad.Get() > .3 && !shooter.InUse)
                ControlCollection.Instance.GetOperatorControl("intakeIn").IsEnabled = false;
            else
                ControlCollection.Instance.GetOperatorControl("intakeIn").IsEnabled = true;
            
        }

        #endregion Protected Methods
    }
}