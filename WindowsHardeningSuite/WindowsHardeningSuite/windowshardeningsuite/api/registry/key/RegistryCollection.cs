using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WindowsHardeningSuite.windowshardeningsuite.api.config;

namespace WindowsHardeningSuite.windowshardeningsuite.api.registry.key
{
    /// <summary>
    /// Main resource class for the reg keys
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class RegistryCollection : Resource
    {
        [JsonProperty]
        public RegistryObject[] RegKeys { get; set; } = new RegistryObject[0];
        [JsonProperty]
        public KeyCategory[] KeyCategories { get; set; } = new KeyCategory[0];


        public List<RegistryObject> RegKeysAsList => new List<RegistryObject>(RegKeys);

        public void Add(RegistryObject registryObject)
        {
            List<RegistryObject> lst = new List<RegistryObject>(RegKeys);
            lst.Add(registryObject);
            RegKeys = lst.ToArray();
        }

        public void SortAlphabetically()
        {
            List<RegistryObject> lst = new List<RegistryObject>(RegKeys);
            lst.Sort((x, y) =>
            {
                string xFull = x.Location + "\\" + x.Location;
                string yFull = y.Location + "\\" + y.Location;
                return String.Compare(xFull, yFull, StringComparison.Ordinal);
            });
        }
    }
}