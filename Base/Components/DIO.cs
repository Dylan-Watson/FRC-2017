using System;
using WPILib;

namespace Base.Components
{
    public enum DIOType
    {
        Input,
        Output
    }

    public class DIO : IO, IComponent
    {
        private readonly DigitalSource dio;

        public bool InUse
        {get; private set;
        }

        public string Name
        {
            get; private set;
        }

        public object Sender
        {
            get; private set;
        }

        public object GetRawComponent()
        {
            return dio;
        }
        public DIO(DIOType type, int channel, string commonName)
        {
            if (type == DIOType.Input)
                dio = new DigitalInput(channel);
            else
                dio = new DigitalOutput(channel);

            Name = commonName;
        }

        public bool Get()
        {
            return ((DigitalInput)dio).Get();
        }

        protected override void set(double val, object sender)
        {
            Sender = sender;
            lock (dio)
            {
                if(dio is DigitalInput)
                {
                    Report.Error("This is a digital input, you cannot output to this.");
                    return;
                }

                if (val == 1 || val == 0)
                {
                    InUse = true;
                    ((DigitalOutput)dio).Set(Convert.ToBoolean(val));
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            Sender = null;
            InUse = false;
        }
    }
}
