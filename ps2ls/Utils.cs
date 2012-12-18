using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ps2ls
{
    public static class Utils
    {
        public static Color GenerateRandomColor(Random random, Color mix)
        {
            Int32 red = random.Next(256);
            Int32 green = random.Next(256);
            Int32 blue = random.Next(256);

            red = (red + mix.R) / 2;
            green = (green + mix.R) / 2;
            blue = (blue + mix.R) / 2;

            return Color.FromArgb(red, green, blue);
        }
    }
}
