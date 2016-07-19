using System.Collections.Generic;
using WPILib;
using WPILib.Interfaces;

namespace Base.Components
{
    internal class CanTalonItem : Motor, IComponent
    {
        private readonly string master;

        private readonly List<CanTalonItem> slaves;
        private readonly CANTalon talon;

        public CanTalonItem(CANTalon talon, string commonName, bool isReversed = false)
        {
            this.talon = talon;
            Name = commonName;
            IsReversed = isReversed;
            talon.MotorControlMode = ControlMode.PercentVbus;
            talon.ControlEnabled = true;
        }

        public CanTalonItem(CANTalon talon, string commonName, double p, double i, double d, bool isReversed = false)
        {
            this.talon = talon;
            Name = commonName;
            IsReversed = isReversed;
            Master = true;
            slaves = new List<CanTalonItem>();
            talon.MotorControlMode = ControlMode.PercentVbus;
            talon.FeedBackDevice = CANTalon.FeedbackDevice.QuadEncoder;
            talon.SetPID(p, i, d);
            talon.ControlEnabled = true;
        }

        public CanTalonItem(CANTalon talon, string commonName, CanTalonItem master, bool isReversed = false)
        {
            this.talon = talon;
            Name = commonName;
            IsReversed = isReversed;
            talon.ReverseOutput(isReversed);
            Slave = true;
            talon.MotorControlMode = ControlMode.Follower;
            master.AddSlave(this);
            this.master = master.Name;
        }

        public bool Master { get; }
        public bool Slave { get; }

        public bool InUse { get; private set; }
        public object Sender { get; private set; }
        public string Name { get; }

        public object GetRawComponent() => talon;

        public void AddSlave(CanTalonItem slave) => slaves.Add(slave);

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
                        if (IsReversed) talon.Set(-val);
                        else talon.Set(val);
                    }
                    else if ((val > Constants.MINUMUM_JOYSTICK_RETURN) && AllowC)
                    {
                        InUse = true;
                        if (IsReversed) talon.Set(-val);
                        else talon.Set(val);
                    }
                    else
                    {
                        talon.ControlEnabled = false;
                        InUse = false;
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
                }
                foreach (var slave in slaves)
                    lock (slave)
                    {
                        ((CANTalon) slave.GetRawComponent()).ControlEnabled = false;
                    }
            }
        }

        public override void Stop()
        {
            lock (talon)
            {
                talon.ControlEnabled = false;
                InUse = false;
            }
        }
    }
}