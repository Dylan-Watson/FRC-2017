using System;
using WPILib;
using WPILib.Extras.NavX;

namespace Base
{
    /// <summary>
    ///     Class that is a NavX instance, this is a singleton
    ///     Use InitializeNavX() to initialize the singleton,
    ///     otherwise the Instance property will return null.
    /// </summary>
    public class NavX : AHRS, IComponent
    {
        #region Private Fields

        private static Lazy<NavX> _lazy;

        #endregion Private Fields

        #region Public Events

        /// <summary>
        ///     Unised, is never invoked
        /// </summary>
        [Obsolete("Unused Property")]
        public event EventHandler ValueChanged;

        #endregion Public Events

        #region Private Methods

        /// <summary>
        ///     Releases managed and native resources
        /// </summary>
        /// <param name="disposing"></param>
        private void dispose(bool disposing)
        {
            if (!disposing) return;
            lock (this)
            {
                base.Dispose();
            }
        }

        #endregion Private Methods

        #region Private Constructors

        private NavX(SPI.Port spiPortId, byte updateRateHz = 50) : base(spiPortId, updateRateHz)
        {
        }

        private NavX(SPI.Port spiPortId, int spiBitrate, byte updateRateHz = 50)
            : base(spiPortId, spiBitrate, updateRateHz)
        {
        }

        private NavX(I2C.Port i2CPortId, byte updateRateHz = 50) : base(i2CPortId, updateRateHz)
        {
        }

        private NavX(SerialPort.Port serialPortId, SerialDataType dataType = SerialDataType.KProcessedData,
            byte updateRateHz = 50) : base(serialPortId, dataType, updateRateHz)
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        ///     Gets the instance (if any) of the NavX AHRS
        /// </summary>
        public static NavX Instance => _lazy?.Value;

        /// <summary>
        ///     Unused, always returns false
        /// </summary>
        [Obsolete("Unused Property")]
        public bool InUse { get; } = false;

        /// <summary>
        ///     Returns the name of this component (always NavX)
        /// </summary>
        public string Name { get; } = "NavX";

        /// <summary>
        ///     Unused, always returns null
        /// </summary>
        [Obsolete("Unused Property")]
        public object Sender { get; } = null;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Disposes of this IComponent and its managed resources
        /// </summary>
        public new void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
            _lazy = null;
        }

        /// <summary>
        ///     Unused, always returns this instance
        /// </summary>
        /// <returns>this</returns>
        [Obsolete("Unused Method")]
        public object GetRawComponent()
        {
            return this;
        }

        #endregion Public Methods

        #region Internal Methods

        /// <summary>
        ///     Initializes the NavX singleton
        /// </summary>
        /// <param name="spiPortId">port that the NavX is plugged into</param>
        /// <param name="updateRateHz">the update rate of the NavX - default 50Hz</param>
        internal static NavX InitializeNavX(SPI.Port spiPortId, byte updateRateHz = 50)
        {
            if (_lazy != null)
                Report.Error(
                    @"A NavX instance already been created, someone other than Config is trying to create an instance.");
            _lazy = new Lazy<NavX>(() => new NavX(spiPortId, updateRateHz));
            return Instance;
        }

        /// <summary>
        ///     Initializes the NavX singleton
        /// </summary>
        /// <param name="spiPortId">port that the NavX is plugged into</param>
        /// <param name="spiBitrate">sets the bitrate for NavX comms</param>
        /// <param name="updateRateHz">the update rate of the NavX - default 50Hz</param>
        internal static NavX InitializeNavX(SPI.Port spiPortId, int spiBitrate, byte updateRateHz = 50)
        {
            if (_lazy != null)
                Report.Error(
                    @"A NavX instance already been created, someone other than Config is trying to create an instance.");
            _lazy = new Lazy<NavX>(() => new NavX(spiPortId, spiBitrate, updateRateHz));
            return Instance;
        }

        /// <summary>
        ///     Initializes the NavX singleton
        /// </summary>
        /// <param name="i2CPortId">port that the NavX is plugged into</param>
        /// <param name="updateRateHz">the update rate of the NavX - default 50Hz</param>
        internal static NavX InitializeNavX(I2C.Port i2CPortId, byte updateRateHz = 50)
        {
            if (_lazy != null)
                Report.Error(
                    @"A NavX instance already been created, someone other than Config is trying to create an instance.");
            _lazy = new Lazy<NavX>(() => new NavX(i2CPortId, updateRateHz));
            return Instance;
        }

        /// <summary>
        ///     Initializes the NavX singleton
        /// </summary>
        /// <param name="serialPortId">port that the NavX is plugged into</param>
        /// <param name="dataType">sets the type of data, processed or raw</param>
        /// <param name="updateRateHz">the update rate of the NavX - default 50Hz</param>
        internal static NavX InitializeNavX(SerialPort.Port serialPortId,
            SerialDataType dataType = SerialDataType.KProcessedData, byte updateRateHz = 50)
        {
            if (_lazy != null)
                Report.Error(
                    @"A NavX instance already been created, someone other than Config is trying to create an instance.");
            _lazy = new Lazy<NavX>(() => new NavX(serialPortId, dataType, updateRateHz));
            return Instance;
        }

        #endregion Internal Methods
    }
}