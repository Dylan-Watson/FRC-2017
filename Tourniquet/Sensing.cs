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
using Base.Components;
using Base.Config;
using WPILib;

namespace Tourniquet
{
    /// <summary>
    ///     Class to handle specific sensor functionality on the robot, this is a ControlLoop, see ControlLoop in Base
    /// </summary>
    public class Sensing : ControlLoop
    {
        private VictorItem intake;
        private DoubleSolenoidItem gm;
        #region Public Constructors

        /// <summary>
        ///     Defult constructor
        /// </summary>
        /// <param name="config">instance of the config</param>
        public Sensing(Config config)
        {
            gm = (DoubleSolenoidItem)config.ActiveCollection.Get(new CommonName(@""));//name?
            intake = (VictorItem)config.ActiveCollection.Get(new CommonName(@""));//name?
            gm.ValueChanged += Gm_ValueChanged;

            CancelSync(this);//remove this if it breaks and try again
        }

        private void Gm_ValueChanged(object sender, System.EventArgs e)
        {
            if (gm.GetCurrentPosition() == DoubleSolenoid.Value.Forward) //or reverse?
            {
                var wd1 = new WatchDog(2000);
                wd1.IsExpired += Wd1_IsExpired;
                wd1.Start();
                intake.Set(1, this);//or -1?
            }
        }

        #endregion Public Constructors

        #region Protected Methods

        /// <summary>
        ///     Instruction to execute within the loop
        /// </summary>
        protected override void main()
        {
           
        }

        private void Wd1_IsExpired(object sender, System.Timers.ElapsedEventArgs e)
        {
            intake.Stop();
        }

        #endregion Protected Methods
    }
}