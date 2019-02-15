using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.IO;

namespace WindowsHardeningSuite.windowshardeningsuite.api.config
{
    /// <summary>
    /// Entry poi nt for all config files.
    /// </summary>
    public class ConfigProvider
    {
        /// <summary>
        /// Provides a config class.
        /// </summary>
        /// <typeparam name="ConfigType">The class of the config type.</typeparam>
        /// <param name="path">Where the file is located</param>
        /// <returns>The loaded config if it exists, or a new config if it doesn't.</returns>
        public static ConfigType Provide<ConfigType>(string path) where ConfigType : Config, new()
        {
            try
            {
                ConfigType thing;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ConfigType));
                using (StreamReader reader = new StreamReader(path))
                {
                    thing = (ConfigType)xmlSerializer.Deserialize(reader);
                }
                return thing;
            }
            catch (Exception e)
            {
                return new ConfigType();
            }
        }

        public static void Save(Config data, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            StreamWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, data);
            writer.Close();
        }
    }
}
