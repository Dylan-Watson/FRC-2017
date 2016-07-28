namespace Base
{
    using System;

    /// <summary>
    /// Abstract class for IO components (not including motors)
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public abstract class IO
    {
        /// <summary>
        ///     Set function that the inheriting component class must implement
        /// </summary>
        /// <param name="val">value to pass to the IO component -1 to +5 valid range</param>
        /// <param name="sender">object calling the method</param>
        protected abstract void set(double val, object sender);

        /// <summary>
        ///     Public set method that calls the inherited classes implementation of protected void set()
        /// </summary>
        /// <param name="val">value to pass to the IO component -1 to +5 valid range</param>
        /// <param name="sender">object calling the method</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     exception is thrown if the input value is out of the acceptable range -1
        ///     to +5
        /// </exception>
        public void Set(double val, object sender)
        {
            if ((val >= -1) && (val <= 5))
                set(val, sender);
            else
                throw new ArgumentOutOfRangeException(nameof(val),
                    "The value provided to the IO device was not within the generalized allowed range (-1 to +5).");
        }

        /// <summary>
        ///     Public set method to handel boolean IO output, simply resolves
        ///     to 1 or -1 then passes to the abstract implementation of set
        /// </summary>
        /// <param name="val">boolean ouput desired</param>
        /// <param name="sender">object calling the method</param>
        public void Set(bool val, object sender) => set(Convert.ToDouble(val), sender);
    }
}