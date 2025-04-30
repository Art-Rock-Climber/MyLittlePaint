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
    public class GrayscaleTransform : IPlugin
    {
        public string Name => "Grayscale_Transform";
        public string NameRus => "Оттенки серого";
        public string Author => "Bannikov_Vladislav";
        public string AuthorRus => "Банников Владислав";

        public void Transform(Bitmap bitmap)
        {
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color color = bitmap.GetPixel(i, j);
                    int grayValue = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
                    bitmap.SetPixel(i, j, grayColor);
                }
            }
        }
    }
}
