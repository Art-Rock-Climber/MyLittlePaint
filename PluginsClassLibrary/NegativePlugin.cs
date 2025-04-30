using PluginInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginsClassLibrary
{
    [Version(1, 0)]
    public class NegativeTransform : IPlugin
    {
        public string Name => "Negative_Transform";
        public string NameRus => "Негатив изображения";
        public string Author => "Bannikov_Vladislav";
        public string AuthorRus => "Банников Владислав";

        public void Transform(Bitmap bitmap)
        {
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color color = bitmap.GetPixel(i, j);
                    Color negative = Color.FromArgb(
                        255 - color.R,
                        255 - color.G,
                        255 - color.B);
                    bitmap.SetPixel(i, j, negative);
                }
            }
        }
    }
}
