using System;
using WPILib;

namespace Base.Components
{
    /// <summary>
    /// </summary>
    public enum DioType
    {
        /// <summary>
        /// </summary>
        Input,

        /// <summary>
        /// </summary>
        Output
    }

    /// <summary>
    /// </summary>
    public class DioItem : IO, IComponent
    {
        private readonly DigitalSource dio;

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="channel"></param>
        /// <param name="commonName"></param>
        public DioItem(DioType type, int channel, string commonName)
        {
            if (type == DioType.Input)
                dio = new DigitalInput(channel);
            else
                dio = new DigitalOutput(channel);

            Name = commonName;
        }

        /// <summary>
        /// </summary>
        public bool InUse { get; private set; }

        /// <summary>
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// </summary>
        public object Sender { get; private set; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public object GetRawComponent()
        {
            return dio;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public bool Get()
        {
            return ((DigitalInput) dio).Get();
        }

        /// <summary>
        /// </summary>
        /// <param name="val"></param>
        /// <param name="sender"></param>
        protected override void set(double val, object sender)
        {
            Sender = sender;
            lock (dio)
            {
                if (dio is DigitalInput)
                {
                    Report.Error("This is a digital input, you cannot output to this.");
                    return;
                }

                if ((val == 1) || (val == 0))
                {
                    InUse = true;
                    ((DigitalOutput) dio).Set(Convert.ToBoolean(val));
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