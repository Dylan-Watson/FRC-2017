/****************************** Header ******************************\
Class Name: Base Calls
Summary: Class to handle all of the calls to the base broject to
gather groups of components
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

//TODO: Ryan, check this header to make sure it is correct

using Base;
using Base.Components;
using Base.Config;
using System;
using System.Linq;
using WPILib;

namespace Trephine
{
    /// <summary>
    ///     Instance based utility class for calles to Base
    /// </summary>
    public sealed class BaseCalls
    {
        #region Private Constructors

        private BaseCalls()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// </summary>
        public static BaseCalls Instance => _lazy.Value;

        private static CommonName gmMani = new CommonName("gm_mani");

        private static CommonName gmRamp = new CommonName("gm_ramp");

        private static CommonName hood = new CommonName("hood");

        private static CommonName intake = new CommonName("intake");

        private static CommonName dt_shifter = new CommonName("dt_shifter");

        private static CommonName shooter_0 = new CommonName("shooter_0");

        private static CommonName shooter_1 = new CommonName("shooter_1");
        
        private static CommonName agitator = new CommonName("agitator");

        private static CommonName climber = new CommonName("climber");

        #endregion Public Properties

        #region Private Fields

        private static readonly Lazy<BaseCalls> _lazy =
            new Lazy<BaseCalls>(() => new BaseCalls());

        private Config config;

        #endregion Private Fields

        #region Public Methods
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="config">instance of the config</param>
        public void SetConfig(Config config)
        {
            this.config = config;
        }

        /// <summary>
        ///     Gets the instance of the config
        /// </summary>
        /// <returns></returns>
        public Config GetConfig() => config;

        /// <summary>
        ///     Full stop of the drive train
        /// </summary>
        public void FullDriveStop()
        {
            config.ActiveCollection.GetRightDriveMotors.ForEach(s => ((Motor) s).Stop());
            config.ActiveCollection.GetLeftDriveMotors.ForEach(s => ((Motor) s).Stop());
        }
        /// <summary>
        ///     Full stop of the robot
        /// </summary>
        public void FullStop()
        {
            config.ActiveCollection.GetRightDriveMotors.ForEach(s => ((Motor) s).Stop());
            config.ActiveCollection.GetLeftDriveMotors.ForEach(s => ((Motor) s).Stop());
            config.ActiveCollection.GetMotorItem(intake).Stop();
        }

        /// <summary>
        ///     Sets the left drive of the robot to a specified value
        /// </summary>
        /// <param name="value">value to set</param>
        public void SetLeftDrive(double value)
            => config.ActiveCollection.GetLeftDriveMotors.ForEach(s => ((Motor) s).Set(value, this));

        /// <summary>
        ///     Sets the right drive of the robot to a specified value
        /// </summary>
        /// <param name="value">value to set</param>
        public void SetRightDrive(double value)
            => config.ActiveCollection.GetRightDriveMotors.ForEach(s => ((Motor) s).Set(value, this));

        /// <summary>
        ///     shift into high/low gear forward=high 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sender"></param>
        public void ShiftGears(DoubleSolenoid.Value value, object sender)
        {
            var tmp = (DoubleSolenoidItem)(config.ActiveCollection.Get(dt_shifter));
            tmp.Set(value, sender);
        }

        /// <summary>
        ///     sets the mani to forward or back position // forward = back
        /// </summary>
        /// <param name="value">value to set</param>
        /// <param name="sender">specifies whats sending it</param>
        public void SetMani(DoubleSolenoid.Value value, object sender)
        {
            var tmp = (DoubleSolenoidItem)(config.ActiveCollection.Get(gmMani));
            tmp.Set(value, sender);
        }

        /// <summary>
        ///     sets gm rampy thingy
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sender"></param>
        public void SetRamp(DoubleSolenoid.Value value, object sender)
        {
            var tmp = (DoubleSolenoidItem)(config.ActiveCollection.Get(gmRamp));
            tmp.Set(value, sender);
        }
        /// <summary>
        ///     sets intake to spin forward/backward 
        /// </summary>
        /// <param name="value">value to set</param>
        /// <param name="sender">speifies whats sending it</param>
        public void SetIntake(double value, object sender)
        {
            var tmp = (Motor)(config.ActiveCollection.Get(intake));
            tmp.Set(value, sender);
        }

        /// <summary>
        /// starts intake
        /// </summary>
        public void StopIntake()
        {
            config.ActiveCollection.GetMotorItem(intake).Stop();
        }

        /// <summary>
        ///     revs up shooter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sender"></param>
        public void StartShooter(double value, object sender)
        {
            var tmp_0 = (Motor)(config.ActiveCollection.Get(shooter_0));
            tmp_0.Set(value, sender);
            var tmp_1 = (Motor)(config.ActiveCollection.Get(shooter_1));
            tmp_1.Set(value, sender);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sender"></param>
        public void StopShooter()
        {
            config.ActiveCollection.GetMotorItem(shooter_0).Stop();
            config.ActiveCollection.GetMotorItem(shooter_1).Stop();
        }

        /// <summary>
        /// starts climber
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sender"></param>
        public void StartCimber(double value, object sender)
        {
            var tmp_0 = (Motor)(config.ActiveCollection.Get(climber));
            tmp_0.Set(value, sender);
        }

        /// <summary>
        /// stops climber lol
        /// </summary>
        public void StopClimber()
        {
            config.ActiveCollection.GetMotorItem(climber).Stop();
        }

        /// <summary>
        /// shifts Da hood
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sender"></param>
        public void ShiftHood(DoubleSolenoid.Value value, object sender)
        {
            var tmp = (DoubleSolenoidItem)(config.ActiveCollection.Get(hood));
            tmp.Set(value, sender);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sender"></param>
        public void StartAgitator(double value, object sender)
        {
            var tmp = (Motor)(config.ActiveCollection.Get(agitator));
            tmp.Set(value, sender);
        }

        /// <summary>
        /// 
        /// </summary>
        public void StopAgitator()
        {
            config.ActiveCollection.GetMotorItem(agitator).Stop();
        }

        /// <summary>
        ///     slow stop dt
        /// </summary>
        public void SlowStop()
        {
            var rightPow = config.ActiveCollection.GetRightDriveMotors.Select(s => ((Motor) s).Get()).ToList()[0];
            var leftPow = config.ActiveCollection.GetLeftDriveMotors.Select(s => ((Motor) s).Get()).ToList()[0];

            while (Math.Abs(rightPow) > .05 && Math.Abs(leftPow) > .05)
            {
                rightPow /= 1.02;
                leftPow /= 1.02;

                SetLeftDrive(leftPow);
                SetRightDrive(rightPow);

                Timer.Delay(.005);
            }

            FullDriveStop();
        }

        /// <summary>
        ///     slow start DT
        /// </summary>
        /// <param name="finalPower"></param>
        public void SlowStart(double finalPower)
        {
            if(finalPower > 0)
            {
                for (double d = .05; d < finalPower; d *= 1.02)
                {
                    SetLeftDrive(d);
                    SetRightDrive(d);
                    Timer.Delay(.005);
                }
                SetLeftDrive(finalPower);
                SetRightDrive(finalPower);
            }
            if(finalPower < 0)
            {
                for (double d = -.05; d > finalPower; d *= 1.02)
                {
                    SetLeftDrive(d);
                    SetRightDrive(d);
                    Timer.Delay(.005);
                }
                SetLeftDrive(finalPower);
                SetRightDrive(finalPower);
            }
        }

        /// <summary>
        ///     slow turn DT
        /// </summary>
        /// <param name="leftPw"></param>
        /// <param name="rightPw"></param>
        public void SlowTurn(double leftPw, double rightPw)
        {
            for(double d = .05 * Math.Sign(leftPw); d < leftPw; d *= 1.02)
            {
                SetLeftDrive(d);
                Timer.Delay(.005);
            }
            for (double d = .05 * Math.Sign(rightPw); d < rightPw; d *= 1.02)
            {
                SetRightDrive(d);
                Timer.Delay(.005);
            }
        }

        /// <summary>
        ///     gets the encoder value of the left drivetrain
        /// </summary>
        /// <returns></returns>
        public VictorItem LeftMotor()
        {
            return (VictorItem)config.ActiveCollection.GetLeftDriveMotors[0];
        }

        /// <summary>
        ///     gets the encoder value of the right drivetrain
        /// </summary>
        /// <returns></returns>
        public VictorItem RightMotor()
        {
            return (VictorItem)config.ActiveCollection.GetRightDriveMotors[0];
        }

        /// <summary>
        ///     uses encoders to make dt run straight around a certain power
        /// </summary>
        /// <param name="right"></param>
        /// <param name="driveTime"></param>
        public void EncoderDrive(double left, double right, double driveTime)
        {
            var wd = new WatchDog(driveTime);
            wd.Start();

            SetLeftDrive(left);
            SetRightDrive(right);
            
            LeftMotor().ResetEncoder();
            RightMotor().ResetEncoder();
            while (wd.State == WatchDog.WatchDogState.Running)
            {
                if (Math.Abs(LeftMotor().GetEncoderValue()) > Math.Abs(RightMotor().GetEncoderValue()))
                    left -= .0001;
                if (Math.Abs(RightMotor().GetEncoderValue()) > Math.Abs(LeftMotor().GetEncoderValue()))
                    left += .0001;

                SetLeftDrive(left);
                SetRightDrive(right);
            }

            SlowStop();
        }
        public void GyroTurn()
        {

        }

        public void DeliverGear(double driveTime, double driveBackTime, double power)
        {
            
            ShiftGears(DoubleSolenoid.Value.Reverse, this);
            Timer.Delay(.25);

            SetLeftDrive(power);
            SetRightDrive(power + .025);
            Timer.Delay(driveTime);

            SetIntake(.6, this);

            SlowStop();

            SetMani(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(.25);

            SlowStart(-power);
            Timer.Delay(driveBackTime);

            FullStop();

            SetMani(DoubleSolenoid.Value.Reverse, this);

            ShiftGears(DoubleSolenoid.Value.Forward, this);
        }

        #endregion Public Methods

        #region Public Dylans

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="power"></param>
        public void driveFullEncoder(double distance, double power)
        {
            LeftMotor().ResetEncoder();
            RightMotor().ResetEncoder();

            double left = power;
            double right = power;
            double temp0 = Math.Abs(LeftMotor().GetEncoderValue()) + Math.Abs(RightMotor().GetEncoderValue());
            double avg =  temp0 / 2;

            while (avg < distance)

            {

                if (Math.Abs(LeftMotor().GetEncoderValue()) > Math.Abs(RightMotor().GetEncoderValue()))
                    left += .00004;
                if (Math.Abs(RightMotor().GetEncoderValue()) > Math.Abs(LeftMotor().GetEncoderValue()))
                    right += .00004;

                SetLeftDrive(left);
                SetRightDrive(right);

                temp0 = Math.Abs(LeftMotor().GetEncoderValue()) + Math.Abs(RightMotor().GetEncoderValue());
                avg = temp0 / 2;
            }

            FullDriveStop();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="power"></param>
        public void turnRightFullEncoder(double distance, double power) {
            LeftMotor().ResetEncoder();
            RightMotor().ResetEncoder();

            double left = power;
            double right = power;

            double lft = Math.Abs(LeftMotor().GetEncoderValue());

            while (lft < distance)
            {
                
                SetLeftDrive(left);
                SetRightDrive(-right);

                lft = Math.Abs(LeftMotor().GetEncoderValue());

            }

            SlowStop();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="power"></param>
        public void turnLeftFullEncoder(double distance, double power)
        {
            LeftMotor().ResetEncoder();
            RightMotor().ResetEncoder();

            double left = power;
            double right = power;

            double rgt = Math.Abs(RightMotor().GetEncoderValue());

            while (rgt < distance)
            {
                SetLeftDrive(-left);
                SetRightDrive(right);

                rgt = Math.Abs(RightMotor().GetEncoderValue());
            }

            SlowStop();
        }

        

        #endregion Public Dylans
    }
}