﻿using System.Collections;
using System.ComponentModel;

namespace BlazorBudget.Wasm.Services
{
    public class ColorService : IColorService
    {
        public List<string> GetColorList()
        {
            return new List<string>
            {
                "#FF6633", "#FFB399", "#FF33FF", "#FFFF99", "#00B3E6",
                "#E6B333", "#3366E6", "#999966", "#99FF99", "#B34D4D",
                "#80B300", "#809900", "#E6B3B3", "#6680B3", "#66991A",
                "#FF99E6", "#CCFF1A", "#FF1A66", "#E6331A", "#33FFCC",
                "#66994D", "#B366CC", "#4D8000", "#B33300", "#CC80CC",
                "#66664D", "#991AFF", "#E666FF", "#4DB3FF", "#1AB399",
                "#E666B3", "#33991A", "#CC9999", "#B3B31A", "#00E680",
                "#4D8066", "#809980", "#E6FF80", "#1AFF33", "#999933",
                "#FF3380", "#CCCC00", "#66E64D", "#4D80CC", "#9900B3",
                "#E64D66", "#4DB380", "#FF4D4D", "#99E6E6", "#6666FF"
            };
        }

        public string GetNewColor(IList<string> existingColors)
        {
            var colors = GetColorList();

            foreach (var color in colors)
            {
                if (!existingColors.Contains(color))
                {
                    return color;
                }
            }

            return "#000000";
        }

        public string GetTextColor(string backgroundColor)
        {
            var color = backgroundColor.Replace("#", string.Empty);
            var r = int.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            var g = int.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            var b = int.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            var brightness = r * 0.299 + g * 0.587 + b * 0.114;
            return brightness > 186 ? "#000000" : "#FFFFFF";
        }
    }

    public interface IColorService
    {
        public string GetNewColor(IList<string> existingColors);
        public string GetTextColor(string backgroundColor);

        public List<string> GetColorList();
    }
}
