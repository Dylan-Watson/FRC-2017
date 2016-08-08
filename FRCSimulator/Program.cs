/****************************** Header ******************************\
Class Name:
Summary:
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using HAL.Simulator;
using WPILib;
using DriverStation = DriverStationGUI.DriverStation;

namespace FRCSimulator
{
    /// <summary>
    /// </summary>
    public static class Program
    {
        #region Public Methods

        /// <summary>
        /// </summary>
        public static void Main() => RobotBase.Main(null, typeof(RobotMain2017.RobotMain2017));

        #endregion Public Methods
    }

    /// <summary>
    /// </summary>
    public class Simulator : ISimulator
    {
        /// <summary>
        /// </summary>
        public string Name => "Mono Game Simulator";

        #region Public Methods

        void ISimulator.Initialize()
        {
        }

        void ISimulator.Start()
        {
            SimHooks.WaitForProgramStart();
            DriverStation.StartDriverStationGui();
            // using (var game = new FRCSimulator()) game.Run();
        }

        #endregion Public Methods
    }
}