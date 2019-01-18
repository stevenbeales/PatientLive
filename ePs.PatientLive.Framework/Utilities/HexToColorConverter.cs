using Windows.UI;

namespace ePs.PatientLive.Framework.Utilities
{
    public static class HexToColorConverter
    {
        /// <summary>
        /// Converts a hexadecimal string value into a Brush.
        /// </summary>
        public static Color ConvertFromHex(string value)
        {
            byte alpha;
            byte pos = 0;

            string hex = value.Replace("#", "");

            if (hex.Length == 8)
            {
                alpha = System.Convert.ToByte(hex.Substring(pos, 2), 16);
                pos = 2;
            }
            else
            {
                alpha = System.Convert.ToByte("ff", 16);
            }

            byte red = System.Convert.ToByte(hex.Substring(pos, 2), 16);

            pos += 2;
            byte green = System.Convert.ToByte(hex.Substring(pos, 2), 16);

            pos += 2;
            byte blue = System.Convert.ToByte(hex.Substring(pos, 2), 16);

            return Color.FromArgb(alpha, red, green, blue);
        }

    }
}
