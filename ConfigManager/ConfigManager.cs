using PluginInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PluginManager
{
    public class PluginConfig
    {
        public string Name { get; set; }
        public string NameRus { get; set; }
        public string AuthorRus { get; set; }
        public string Version { get; set; }
        public bool Enabled { get; set; }

    }

    public class AppConfig
    {
        public bool AutoMode { get; set; } = true;
        public List<PluginConfig> Plugins { get; set; } = new List<PluginConfig>();
    }

    public static class ConfigManager
    {
        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins_config.xml");

        public static AppConfig LoadConfig(List<IPlugin> allPlugins)
        {
            AppConfig config;

            if (!File.Exists(ConfigPath))
            {
                config = CreateDefaultConfig(allPlugins);
                SaveConfig(config);
                return config;
            }

            try
            {
                var serializer = new XmlSerializer(typeof(AppConfig));
                using (var reader = new StreamReader(ConfigPath))
                {
                    config = (AppConfig)serializer.Deserialize(reader);
                }

                UpdatePluginList(config, allPlugins);
                return config;
            }
            catch
            {
                // Если файл поврежден, создаем новый
                config = CreateDefaultConfig(allPlugins);
                SaveConfig(config);
                return config;
            }
        }

        private static AppConfig CreateDefaultConfig(List<IPlugin> plugins)
        {
            return new AppConfig
            {
                AutoMode = true,
                Plugins = plugins.Select(p => new PluginConfig
                {
                    Name = p.Name,
                    NameRus = p.NameRus,
                    AuthorRus = p.AuthorRus,
                    Version = GetPluginVersion(p),
                    Enabled = true
                }).ToList()
            };
        }

        private static void UpdatePluginList(AppConfig config, List<IPlugin> plugins)
        {
            // Добавляем новые плагины
            foreach (var plugin in plugins)
            {
                if (!config.Plugins.Any(p => p.Name == plugin.Name))
                {
                    config.Plugins.Add(new PluginConfig
                    {
                        Name = plugin.Name,
                        NameRus = plugin.NameRus,
                        AuthorRus = plugin.AuthorRus,
                        Version = GetPluginVersion(plugin),
                        Enabled = true
                    });
                }
            }

            //// Удаляем больше не существующие плагины
            //var toRemove = config.Plugins.Where(p => !plugins.Any(pl => pl.Name == p.Name)).ToList();
            //foreach (var item in toRemove)
            //{
            //    config.Plugins.Remove(item);
            //}
        }

        public static void SaveConfig(AppConfig config)
        {
            var serializer = new XmlSerializer(typeof(AppConfig));
            using (var writer = new StreamWriter(ConfigPath))
            {
                serializer.Serialize(writer, config);
            }
        }

        private static string GetPluginVersion(IPlugin plugin)
        {
            var type = plugin.GetType();
            var attr = (VersionAttribute)Attribute.GetCustomAttribute(type, typeof(VersionAttribute));
            return attr != null ? $"{attr.Major}.{attr.Minor}" : "1.0";
        }
    }
}
