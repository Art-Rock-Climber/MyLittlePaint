using PluginInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Device.Location;
using System.Net;
using System.Text.Json;


namespace PluginsClassLibrary
{
    [Version(1, 0)]
    public class AddDateLocationTransform : IPlugin
    {
        public string Name => "Add_Timestamp";
        public string NameRus => "Добавить дату и геолокацию";
        public string Author => "Bannikov_Vladislav";
        public string AuthorRus => "Банников Владислав";

        public void Transform(Bitmap bitmap)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                string text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                string location = GetLocation();

                if (!string.IsNullOrEmpty(location))
                {
                    text += $"\n{location}";
                }

                Font font = new Font("Arial", 12, FontStyle.Bold);
                SizeF textSize = g.MeasureString(text, font);

                // Позиция в правом нижнем углу с отступом
                PointF position = new PointF(
                    bitmap.Width - textSize.Width - 10,
                    bitmap.Height - textSize.Height - 10);

                // Фон для текста
                g.FillRectangle(Brushes.White,
                    position.X - 2, position.Y - 2,
                    textSize.Width + 4, textSize.Height + 4);

                g.DrawString(text, font, Brushes.Black, position);
            }
        }

        private string GetLocation()
        {
            try
            {
                //using (var watcher = new GeoCoordinateWatcher())
                //{
                //    watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));
                //    if (!watcher.Position.Location.IsUnknown)
                //    {
                //        return $"{watcher.Position.Location.Latitude}, {watcher.Position.Location.Longitude}";
                //    }
                //}
                //return "Без локации"; // Заглушка

                using (var client = new WebClient())
                {
                    // Запрос к бесплатному API
                    client.Encoding = System.Text.Encoding.UTF8;
                    string json = client.DownloadString("http://ip-api.com/json/?lang=ru");
                    dynamic data = JsonSerializer.Deserialize<LocationData>(json);
                    if (!string.IsNullOrWhiteSpace(data?.country))
                    {
                        if (!string.IsNullOrWhiteSpace(data.city))
                        {
                            return $"{data.city}, {data.country}";
                        }
                        else return data.country;
                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }

    public class LocationData
    {
        // Имена свойств не менять! Иначе json не распознает
        public string city { get; set; }
        public string country { get; set; }
    }
}
