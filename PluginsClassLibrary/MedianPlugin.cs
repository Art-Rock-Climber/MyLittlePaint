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
    public class MedianFilter : IPlugin
    {
        public string Name => "Median_Filter";
        public string NameRus => "Медианный фильтр";
        public string Author => "Bannikov_Vladislav";
        public string AuthorRus => "Банников Владислав";

        public void Transform(Bitmap bitmap)
        {
            Bitmap temp = (Bitmap)bitmap.Clone();

            for (int i = 1; i < bitmap.Width - 1; i++)
            {
                for (int j = 1; j < bitmap.Height - 1; j++)
                {
                    List<Color> neighbors = new List<Color>();

                    // Собираем цвета соседних пикселей (окно 3x3)
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            neighbors.Add(temp.GetPixel(i + x, j + y));
                        }
                    }

                    // Сортируем и берем медиану
                    var sortedR = neighbors.Select(c => c.R).OrderBy(v => v).ToList();
                    var sortedG = neighbors.Select(c => c.G).OrderBy(v => v).ToList();
                    var sortedB = neighbors.Select(c => c.B).OrderBy(v => v).ToList();

                    int medianR = sortedR[4];
                    int medianG = sortedG[4];
                    int medianB = sortedB[4];

                    bitmap.SetPixel(i, j, Color.FromArgb(medianR, medianG, medianB));
                }
            }
        }
    }
}
