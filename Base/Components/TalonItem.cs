using System;
using System.Collections.Generic;
using WPILib;
using WPILib.Interfaces;

namespace Base.Components
{
    /// <summary>
    /// Class to handle CanTalon motor controllers
    /// </summary>
    public class CanTalonItem : Motor, IComponent
    {
        #region Public Events

        /// <summary>
        /// Event used for VirtualControlEvents
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion Public Events

        #region Protected Methods

        /// <summary>
        /// Method to fire value changes for set/get values and InUse values
        /// </summary>
        /// <param name="e">VirtualControlEventArgs</param>
        protected virtual void onValueChanged(VirtualControlEventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        #endregion Protected Methods

        #region Private Fields

        private readonly string master;

        private readonly List<CanTalonItem> slaves;
        private readonly CANTalon talon;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructor
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
        /// PID Constructor
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
        /// Slave Constructor
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
        /// Defines if the talon is in use
        /// </summary>
        public bool InUse { get; private set; }

        /// <summary>
        /// Defines if the talon is a master
        /// </summary>
        public bool Master { get; }

        /// <summary>
        /// Name of the component
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Defines the object issuing the commands
        /// </summary>
        public object Sender { get; private set; }

        /// <summary>
        /// Defines if the talon is a slave
        /// </summary>
        public bool Slave { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Adds a slave to this controller
        /// </summary>
        /// <param name="slave"></param>
        public void AddSlave(CanTalonItem slave) => slaves.Add(slave);

        /// <summary>
        /// Gets the raw WPI CANTalon object
        /// </summary>
        /// <returns></returns>
        public object GetRawComponent() => talon;

        /// <summary>
        /// Sets a value to the talon
        /// </summary>
        /// <param name="val">value to set the controller to</param>
        /// <param name="sender">the caller of this method</param>
        public override void Set(double val, object sender)
        {
            Sender = sender;
            if (Slave)
            {
                Report.Warning(
                    $"Someone is trying to set {Name}, but {Name} is a slave, it can only be set by its master {master}.");
                return;
            }

            if (!Master)
                lock (talon)
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
                    lock (talon)
                    {
                        talon.ControlEnabled = true;
                        InUse = true;
                        talon.Set(-val);
                        onValueChanged(new VirtualControlEventArgs(-val, InUse));
                    }
                    foreach (var slave in slaves)
                        lock (slave)
                        {
                            ((CANTalon) slave.GetRawComponent()).ControlEnabled = true;
                            ((CANTalon) slave.GetRawComponent()).Set(talon.DeviceId);
                        }
                }
                else
                {
                    lock (talon)
                    {
                        talon.ControlEnabled = true;
                        InUse = true;
                        talon.Set(val);
                        onValueChanged(new VirtualControlEventArgs(val, InUse));
                    }
                    foreach (var slave in slaves)
                        lock (slave)
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
                    lock (talon)
                    {
                        talon.ControlEnabled = true;
                        InUse = true;
                        talon.Set(-val);
                        onValueChanged(new VirtualControlEventArgs(-val, InUse));
                    }
                    foreach (var slave in slaves)
                        lock (slave)
                        {
                            ((CANTalon) slave.GetRawComponent()).ControlEnabled = true;
                            ((CANTalon) slave.GetRawComponent()).Set(talon.DeviceId);
                        }
                }
                else
                {
                    lock (talon)
                    {
                        talon.ControlEnabled = true;
                        InUse = true;
                        talon.Set(val);
                        onValueChanged(new VirtualControlEventArgs(val, InUse));
                    }
                    foreach (var slave in slaves)
                        lock (slave)
                        {
                            ((CANTalon) slave.GetRawComponent()).ControlEnabled = true;
                            ((CANTalon) slave.GetRawComponent()).Set(talon.DeviceId);
                        }
                }
            }
            else
            {
                lock (talon)
                {
                    talon.ControlEnabled = false;
                    InUse = false;
                    onValueChanged(new VirtualControlEventArgs(-val, InUse));
                }
                foreach (var slave in slaves)
                    lock (slave)
                    {
                        ((CANTalon) slave.GetRawComponent()).ControlEnabled = false;
                    }
            }
        }

        /// <summary>
        /// Stops the controller
        /// </summary>
        public override void Stop()
        {
            lock (talon)
            {
                talon.ControlEnabled = false;
                InUse = false;
                Sender = null;
                onValueChanged(new VirtualControlEventArgs(0, InUse));
            }
        }

        #endregion Public Methods
    }
}