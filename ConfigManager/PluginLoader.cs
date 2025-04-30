using PluginInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginManager
{
    public static class PluginLoader
    {
        public static List<IPlugin> LoadPlugins(string pluginsPath)
        {
            var plugins = new List<IPlugin>();

            if (!Directory.Exists(pluginsPath))
                return plugins;

            foreach (var dll in Directory.GetFiles(pluginsPath, "*.dll"))
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dll);
                    foreach (var type in assembly.GetTypes())
                    {
                        if (typeof(IPlugin).IsAssignableFrom(type))
                        {
                            var plugin = (IPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки плагина {dll}: {ex.Message}");
                }
            }

            // Загружаем конфигурацию и фильтруем плагины
            var config = ConfigManager.LoadConfig(plugins);

            return config.AutoMode
                ? plugins
                : plugins.Where(p => config.Plugins.FirstOrDefault(c => c.Name == p.Name)?.Enabled ?? true).ToList();
        }
    }
}
