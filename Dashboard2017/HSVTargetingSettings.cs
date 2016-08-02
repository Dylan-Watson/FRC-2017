using System;

namespace Dashboard2017
{
    public sealed class HsvTargetingSettings
    {
        private static readonly Lazy<HsvTargetingSettings> _lazy =
            new Lazy<HsvTargetingSettings>(() => new HsvTargetingSettings());

        private HsvTargetingSettings()
        {
        }

        public static HsvTargetingSettings Instance => _lazy.Value;

        public int TargetRightXBound { get; set; }
        public int TargetLeftXBound { get; set; }
        public int TargetRadiusUpperBound { get; set; }
        public int TargetRadiusLowerBound { get; set; }

        public uint LowerHue { get; set; }
        public uint UpperHue { get; set; }
        public uint LowerValue { get; set; }
        public uint UpperValue { get; set; }
        public uint LowerSaturation { get; set; }
        public uint UpperSaturation { get; set; }
    }
}