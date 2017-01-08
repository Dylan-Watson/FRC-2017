/****************************** Header ******************************\
Class Name: CanTalonItem inherits Motor and IComponent
Summary: Abstraction for the WPIlib CANTalon that extends to include
some helper and control methods.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

/*using System;
using System.Collections.Generic;
using WPILib;
using WPILib.Interfaces;

namespace Base.Components
{
    /// <summary>
    ///     Class to handle CanTalon motor controllers
    /// </summary>
    public sealed class CanTalonItem : Motor, IComponent
    {
        #region Public Events

        /// <summary>
        ///     Event used for VirtualControlEvents
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion Public Events

        #region Private Fields

        private readonly string master;

        private readonly List<CanTalonItem> slaves;

        private readonly CANTalon talon;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="channel">channel/address of the talon</param>
        /// <param name="commonName">CommonName the component will have</param>
        /// <param name="isReversed">if the controller output should be reversed</param>
        public CanTalonItem(int channel, string commonName, bool isReversed = false)
        {
            talon = new CANTalon(channel);
            Name = commonName;
            IsReversed = isReversed;
            talon.MotorControlMode = ControlMode.PercentVbus;
            talon.ControlEnabled = true;
        }

        /// <summary>
        ///     PID Constructor
        /// </summary>
        /// <param name="channel">channel/address of the talon</param>
        /// <param name="commonName">CommonName the component will have</param>
        /// <param name="p">proportion</param>
        /// <param name="i">integral</param>
        /// <param name="d">derivative</param>
        /// <param name="isReversed">if the controller output should be reversed</param>
        public CanTalonItem(int channel, string commonName, double p, double i, double d, bool isReversed = false)
        {
            talon = new CANTalon(channel);
            Name = commonName;
            IsReversed = isReversed;
            Master = true;
            slaves = new List<CanTalonItem>();
            talon.MotorControlMode = ControlMode.PercentVbus;
            talon.FeedBackDevice = CANTalon.FeedbackDevice.QuadEncoder;
            talon.SetPID(p, i, d);
            talon.ControlEnabled = true;
        }

        /// <summary>
        ///     Slave Constructor
        /// </summary>
        /// <param name="channel">channel/address of the talon</param>
        /// <param name="commonName">CommonName the component will have</param>
        /// <param name="master">master talon (this is the slave)</param>
        /// <param name="isReversed">if the controller output should be reversed</param>
        public CanTalonItem(int channel, string commonName, CanTalonItem master, bool isReversed = false)
        {
            talon = new CANTalon(channel);
            Name = commonName;
            IsReversed = isReversed;
            talon.ReverseOutput(isReversed);
            Slave = true;
            talon.MotorControlMode = ControlMode.Follower;
            master.AddSlave(this);
            this.master = master.Name;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        ///     Defines if the talon is in use
        /// </summary>
        public bool InUse { get; private set; }

        /// <summary>
        ///     Defines if the talon is a master
        /// </summary>
        public bool Master { get; }

        /// <summary>
        ///     Name of the component
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Defines the object issuing the commands
        /// </summary>
        public object Sender { get; private set; }

        /// <summary>
        ///     Defines if the talon is a slave
        /// </summary>
        public bool Slave { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Adds a slave to this controller
        /// </summary>
        /// <param name="slave"></param>
        public void AddSlave(CanTalonItem slave) => slaves.Add(slave);

        /// <summary>
        ///     Disposes of this IComponent and its managed resources
        /// </summary>
        public void Dispose()
        {
            dispose(true);
            //GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Returns the current value of the encoder
        /// </summary>
        /// <returns></returns>
        public double GetEncoderValue()
        {
            return encoder.Get();
        }

        /// <summary>
        ///     Gets the raw WPI CANTalon object
        /// </summary>
        /// <returns></returns>
        public object GetRawComponent() => talon;

        /// <summary>
        ///     Sets a value to the talon
        /// </summary>
        /// <param name="val">value to set the controller to</param>
        /// <param name="sender">the caller of this method</param>
        public override void Set(double val, object sender)
        {
            Sender = sender;
            SetAllowC(upperLimit?.GetBool() ?? true);
            SetAllowCc(lowerLimit?.GetBool() ?? true);
            if (Slave)
            {
                Report.Warning(
                    $"Someone is trying to set {Name}, but {Name} is a slave, it can only be set by its master {master}.");
                return;
            }

            if (!Master)
#if USE_LOCKING
                lock (talon)
#endif
                {
                    talon.ControlEnabled = true;
                    if ((val < -Constants.MINUMUM_JOYSTICK_RETURN) && AllowCc)
                    {
                        InUse = true;
                        if (IsReversed)
                        {
                            talon.Set(-val);
                            onValueChanged(new VirtualControlEventArgs(-val, InUse));
                        }
                        else
                        {
                            talon.Set(val);
                            onValueChanged(new VirtualControlEventArgs(val, InUse));
                        }
                    }
                    else if ((val > Constants.MINUMUM_JOYSTICK_RETURN) && AllowC)
                    {
                        InUse = true;
                        if (IsReversed)
                        {
                            talon.Set(-val);
                            onValueChanged(new VirtualControlEventArgs(-val, InUse));
                        }
                        else
                        {
                            talon.Set(val);
                            onValueChanged(new VirtualControlEventArgs(val, InUse));
                        }
                    }
                    else
                    {
                        talon.Set(0);
                        talon.ControlEnabled = false;
                        InUse = false;
                        onValueChanged(new VirtualControlEventArgs(val, InUse));
                    }
                }
            if (!Master) return;

            if ((val < Constants.MINUMUM_JOYSTICK_RETURN) && AllowCc)
            {
                if (IsReversed)
                {
#if USE_LOCKING
                    lock (talon)
#endif
                    {
                        talon.ControlEnabled = true;
                        InUse = true;
                        talon.Set(-val);
                        onValueChanged(new VirtualControlEventArgs(-val, InUse));
                    }
                    foreach (var slave in slaves)
#if USE_LOCKING
                        lock (slave)
#endif
                        {
                            ((CANTalon) slave.GetRawComponent()).ControlEnabled = true;
                            ((CANTalon) slave.GetRawComponent()).Set(talon.DeviceId);
                        }
                }
                else
                {
#if USE_LOCKING
                    lock (talon)
#endif
                    {
                        talon.ControlEnabled = true;
                        InUse = true;
                        talon.Set(val);
                        onValueChanged(new VirtualControlEventArgs(val, InUse));
                    }
                    foreach (var slave in slaves)
#if USE_LOCKING
                        lock (slave)
#endif
                        {
                            ((CANTalon) slave.GetRawComponent()).ControlEnabled = true;
                            ((CANTalon) slave.GetRawComponent()).Set(talon.DeviceId);
                        }
                }
            }
            else if ((val > Constants.MINUMUM_JOYSTICK_RETURN) && AllowC)
            {
                if (IsReversed)
                {
#if USE_LOCKING
                    lock (talon)
#endif
                    {
                        talon.ControlEnabled = true;
                        InUse = true;
                        talon.Set(-val);
                        onValueChanged(new VirtualControlEventArgs(-val, InUse));
                    }
                    foreach (var slave in slaves)
#if USE_LOCKING
                        lock (slave)
#endif
                        {
                            ((CANTalon) slave.GetRawComponent()).ControlEnabled = true;
                            ((CANTalon) slave.GetRawComponent()).Set(talon.DeviceId);
                        }
                }
                else
                {
#if USE_LOCKING
                    lock (talon)
#endif
                    {
                        talon.ControlEnabled = true;
                        InUse = true;
                        talon.Set(val);
                        onValueChanged(new VirtualControlEventArgs(val, InUse));
                    }
                    foreach (var slave in slaves)
#if USE_LOCKING
                        lock (slave)
#endif
                        {
                            ((CANTalon) slave.GetRawComponent()).ControlEnabled = true;
                            ((CANTalon) slave.GetRawComponent()).Set(talon.DeviceId);
                        }
                }
            }
            else
            {
#if USE_LOCKING
                lock (talon)
#endif
                {
                    talon.Set(0);
                    talon.ControlEnabled = false;
                    InUse = false;
                    onValueChanged(new VirtualControlEventArgs(-val, InUse));
                }
                foreach (var slave in slaves)
#if USE_LOCKING
                    lock (slave)
#endif
                    {
                        ((CANTalon) slave.GetRawComponent()).ControlEnabled = false;
                    }
            }
        }

        /// <summary>
        ///     Attach an encoder to this motor
        /// </summary>
        /// <param name="encoder">The EncoderItem to bind to the motor</param>
        public void SetEncoder(EncoderItem encoder)
        {
            base.encoder = encoder;
        }

        /// <summary>
        ///     Attach a DigitalInputItem to be the lowerlimit of this motor
        /// </summary>
        /// <param name="lowerLimit">The DigitalInputItem to attach</param> 
        public void SetLowerLimit(DigitalInputItem lowerLimit)
        {
            base.lowerLimit = lowerLimit;
        }

        /// <summary>
        ///     Attach a DigitalInputItem to be the upperlimit of this motor
        /// </summary>
        /// <param name="upperLimit">The DigitalInputItem to attach</param>
        public void SetUpperLimit(DigitalInputItem upperLimit)
        {
            base.upperLimit = upperLimit;
        }

        /// <summary>
        ///     Stops the controller
        /// </summary>
        public override void Stop()
        {
#if USE_LOCKING
            lock (talon)
#endif
            {
                talon.Set(0);
                talon.ControlEnabled = false;
                InUse = false;
                Sender = null;
                onValueChanged(new VirtualControlEventArgs(0, InUse));
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Releases managed and native resources
        /// </summary>
        /// <param name="disposing"></param>
        private void dispose(bool disposing)
        {
            if (!disposing) return;
#if USE_LOCKING
            lock (talon)
#endif
            {
                talon?.Dispose();
            }
        }

        /// <summary>
        ///     Method to fire value changes for set/get values and InUse values
        /// </summary>
        /// <param name="e">VirtualControlEventArgs</param>
        private void onValueChanged(VirtualControlEventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        #endregion Private Methods
    }
}*/

using System;

namespace Base.Components
{
    public sealed class CanTalonItem : Motor, IComponent
    {
        public CanTalonItem(int i, string n)
        {
            throw new NotImplementedException();
        }

        public CanTalonItem(int channel, string commonName, double p, double i, double d, bool isReversed = false)
        {
            throw new NotImplementedException();
        }

        public CanTalonItem(int channel, string commonName, CanTalonItem master, bool isReversed = false)
        {
            throw new NotImplementedException();
        }

        public void SetEncoder(EncoderItem encoder)
        {
            base.encoder = encoder;
        }

        public void SetLowerLimit(DigitalInputItem lowerLimit)
        {
            base.lowerLimit = lowerLimit;
        }

        public void SetUpperLimit(DigitalInputItem upperLimit)
        {
            base.upperLimit = upperLimit;
        }

        public bool InUse
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public object Sender
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler ValueChanged;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object GetRawComponent()
        {
            throw new NotImplementedException();
        }

        public override void Set(double val, object sender)
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}