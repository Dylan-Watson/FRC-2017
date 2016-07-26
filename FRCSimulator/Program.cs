/****************************** Header ******************************\
Class Name:
Summary:
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

namespace FRCSimulator
{
    using HAL.Simulator;
    using RobotMain2017;
    using WPILib;
    using DriverStation = DriverStationGUI.DriverStation;

    public static class Program
    {
        public static void Main() => RobotBase.Main(null, typeof(RobotMain2017));
    }

    public class Simulator : ISimulator
    {
        public void Initialize()
        {
        }

        public void Start()
        {
            SimHooks.WaitForProgramStart();
            DriverStation.StartDriverStationGui();
            // using (var game = new FRCSimulator())
            //  game.Run();
        }

        public string Name => "Mono Game Simulator";
    }
}