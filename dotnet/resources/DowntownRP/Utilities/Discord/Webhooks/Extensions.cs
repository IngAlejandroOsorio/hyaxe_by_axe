using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace DowntownRP.Utilities.Discord.Webhooks
{
    public static class Extensions
    {
        public static int ToRgb(this Color color)
        {
            return int.Parse(ColorTranslator.ToHtml(Color.FromArgb(color.ToArgb())).Replace("#", ""), NumberStyles.HexNumber);
        }
    }
}
