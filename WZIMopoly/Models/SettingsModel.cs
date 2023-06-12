using System;
using System.Text;
using System.Text.Json.Serialization;
using WZIMopoly.Enums;

namespace WZIMopoly.Models
{
    /// <summary>
    /// Represents the settings model.
    /// </summary>
    [Serializable]
    internal class SettingsModel : Model
    {
        [JsonIgnore]
        public static Resolution Resolution { get; set; }
        public static bool IsWindowed { get; set; }
        public static float EffectVolume { get; set; }
        public static float SongVolume { get; set; }
        [JsonIgnore]
        public static Language Language { get; set; }
    }
}
