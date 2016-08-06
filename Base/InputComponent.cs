namespace Base
{
    /// <summary>
    /// Abstract class for creating components that have physical input
    /// </summary>
    public abstract class InputComponent
    {
        /// <summary>
        /// Gets the primary input value from the component
        /// </summary>
        /// <returns></returns>
        public abstract double Get();
    }
}
