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
    public class ReverseTransform : IPlugin
    {
        public string Name => "Reverse_Transform";
        public string NameRus => "Переворот изображения";
        public string Author => "Bannikov_Vladislav";
        public string AuthorRus => "Банников Владислав";

        public void Transform(Bitmap bitmap)
        {
            for (int i = 0; i < bitmap.Width; ++i)
                for (int j = 0; j < bitmap.Height / 2; ++j)
                {
                    Color color = bitmap.GetPixel(i, j);
                    bitmap.SetPixel(i, j, bitmap.GetPixel(i, bitmap.Height - j - 1));
                    bitmap.SetPixel(i, bitmap.Height - j - 1, color);
                }
        }
    }
}
