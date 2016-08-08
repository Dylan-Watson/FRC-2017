using System;

namespace Dashboard2017
{
    public sealed class HsvTargetingSettings
    {
        #region Private Fields

        private static readonly Lazy<HsvTargetingSettings> _lazy =
            new Lazy<HsvTargetingSettings>(() => new HsvTargetingSettings());

        #endregion Private Fields

        #region Private Constructors

        private HsvTargetingSettings()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        public static HsvTargetingSettings Instance => _lazy.Value;

        public uint LowerHue { get; set; }
        public uint LowerSaturation { get; set; }
        public uint LowerValue { get; set; }
        public int TargetLeftXBound { get; set; }
        public int TargetRadiusLowerBound { get; set; }
        public int TargetRadiusUpperBound { get; set; }
        public int TargetRightXBound { get; set; }
        public uint UpperHue { get; set; }
        public uint UpperSaturation { get; set; }
        public uint UpperValue { get; set; }

        #endregion Public Properties
    }
}