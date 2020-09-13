using System;
using Newtonsoft.Json;

namespace Versl.Models
{
    public class ColorConfiguration
    {
        public ColorConfiguration()
        {
            //Default Values
            LightPrimaryColor = "#EEEEEE";
            LightPrimaryVariantColor = "#708090";
            LightSecondaryColor = "#EEEEEE";
            LightSecondaryVariantColor = "#EEEEEE";
            LightBackgroundColor = "#EEEEEE";
            LightSurfaceColor = "#EEEEEE";
            LightErrorColor = "#EEEEEE";
            LightOnPrimaryColor = "#000000";
            LightOnPrimaryVariantColor = "#000000";
            LightOnSecondaryColor = "#000000";
            LightOnSecondaryVariantColor = "#000000";
            LightOnBackgroundColor = "#000000";
            LightOnSurfaceColor = "#000000";
            LightOnErrorColor = "#000000";

            DarkPrimaryColor = "#000000"; //
            DarkPrimaryVariantColor = "#708090";
            DarkSecondaryColor = "#EEEEEE";
            DarkSecondaryVariantColor = "#EEEEEE";
            DarkBackgroundColor = "#000000"; //
            DarkSurfaceColor = "#000000";
            DarkErrorColor = "#EEEEEE";
            DarkOnPrimaryColor = "#EEEEEE"; //
            DarkOnPrimaryVariantColor = "#EEEEEE";
            DarkOnSecondaryColor = "#EEEEEE";
            DarkOnSecondaryVariantColor = "#EEEEEE";
            DarkOnBackgroundColor = "#EEEEEE"; //
            DarkOnSurfaceColor = "#EEEEEE";
            DarkOnErrorColor = "#EEEEEE";

            VerslPurple = "#8008B1";
            VerslTeal = "#68CAB7";
        }

        #region Light
        [JsonProperty("lightPrimaryColor")]
        public string LightPrimaryColor { get; set; }

        [JsonProperty("lightPrimaryVariantColor")]
        public string LightPrimaryVariantColor { get; set; }

        [JsonProperty("lightSecondaryColor")]
        public string LightSecondaryColor { get; set; }

        [JsonProperty("lightSecondaryVariantColor")]
        public string LightSecondaryVariantColor { get; set; }

        [JsonProperty("lightBackgroundColor")]
        public string LightBackgroundColor { get; set; }

        [JsonProperty("lightSurfaceColor")]
        public string LightSurfaceColor { get; set; }

        [JsonProperty("lightErrorColor")]
        public string LightErrorColor { get; set; }

        [JsonProperty("lightOnPrimaryColor")]
        public string LightOnPrimaryColor { get; set; }

        [JsonProperty("lightOnPrimaryVariantColor")]
        public string LightOnPrimaryVariantColor { get; set; }

        [JsonProperty("lightOnSecondaryColor")]
        public string LightOnSecondaryColor { get; set; }

        [JsonProperty("lightOnSecondaryVariantColor")]
        public string LightOnSecondaryVariantColor { get; set; }

        [JsonProperty("lightOnBackgroundColor")]
        public string LightOnBackgroundColor { get; set; }

        [JsonProperty("lightOnSurfaceColor")]
        public string LightOnSurfaceColor { get; set; }

        [JsonProperty("lightOnErrorColor")]
        public string LightOnErrorColor { get; set; }
        #endregion

        #region Dark
        [JsonProperty("darkPrimaryColor")]
        public string DarkPrimaryColor { get; set; }

        [JsonProperty("darkPrimaryVariantColor")]
        public string DarkPrimaryVariantColor { get; set; }

        [JsonProperty("darkSecondaryColor")]
        public string DarkSecondaryColor { get; set; }

        [JsonProperty("darkSecondaryVariantColor")]
        public string DarkSecondaryVariantColor { get; set; }

        [JsonProperty("darkBackgroundColor")]
        public string DarkBackgroundColor { get; set; }

        [JsonProperty("darkSurfaceColor")]
        public string DarkSurfaceColor { get; set; }

        [JsonProperty("darkErrorColor")]
        public string DarkErrorColor { get; set; }

        [JsonProperty("darkOnPrimaryColor")]
        public string DarkOnPrimaryColor { get; set; }

        [JsonProperty("darkOnPrimaryVariantColor")]
        public string DarkOnPrimaryVariantColor { get; set; }

        [JsonProperty("darkOnSecondaryColor")]
        public string DarkOnSecondaryColor { get; set; }

        [JsonProperty("darkOnSecondaryVariantColor")]
        public string DarkOnSecondaryVariantColor { get; set; }

        [JsonProperty("darkOnBackgroundColor")]
        public string DarkOnBackgroundColor { get; set; }

        [JsonProperty("darkOnSurfaceColor")]
        public string DarkOnSurfaceColor { get; set; }

        [JsonProperty("darkOnErrorColor")]
        public string DarkOnErrorColor { get; set; }
        #endregion

        [JsonIgnore]
        public string VerslPurple { get; private set; }

        [JsonIgnore]
        public string VerslTeal { get; private set; }
    }
}
