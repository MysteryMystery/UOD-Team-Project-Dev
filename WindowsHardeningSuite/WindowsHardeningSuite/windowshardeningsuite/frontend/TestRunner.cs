using System;
using WindowsHardeningSuite.Properties;
using WindowsHardeningSuite.windowshardeningsuite.api.config;
using WindowsHardeningSuite.windowshardeningsuite.api.registry.key;
using Newtonsoft.Json;

namespace WindowsHardeningSuite.windowshardeningsuite.frontend
{
    public class TestRunner
    {
        public static void Main(string[] args)
        {
            //RegistryObject registryObject = ResourceProvider.ProvideJSON<RegistryObject>(Properties.Resources.keys);
            LoadAppResource();
        }

        private static void LoadAppResource()
        {
            RegistryCollection registryCollection = ResourceProvider.ProvideJSON<RegistryCollection>(Resources.keys);
            foreach (var key in registryCollection.RegKeys)
            {
                Console.WriteLine(key.DisplayName);
            }
        } 

        private static void CreateAppResource()
        {
            RegistryObject registryObject = new RegistryObject();
            registryObject.ValueKind = "Value Kind";
            registryObject.ValueType = "Value Type";
            registryObject.WindowsVersions = new string[]
            {
                "Value 1",
                "Value 2"
            };
            registryObject.Location = "location";
            registryObject.ID = "Test Key";
            registryObject.DisplayName = "Test Display Key";
            registryObject.RecommendedValue = "Value 1";
            registryObject.DisplayDescription = "Test display desc";
            
            RegistryCollection registryCollection = new RegistryCollection();
            registryCollection.Add(registryObject);
            
            Console.WriteLine(JsonConvert.SerializeObject(registryCollection));
        }
    }
}