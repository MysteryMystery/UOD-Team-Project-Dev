using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WindowsHardeningSuite.windowshardeningsuite.api.registry.key
{
    /// <summary>
    /// Main resource class for the reg keys
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class RegistryCollection
    {
        [JsonProperty]
        public RegistryObject[] RegKeys { get; set; } = new RegistryObject[0];


        public List<RegistryObject> RegKeysAsList => new List<RegistryObject>(RegKeys);

        public void Add(RegistryObject registryObject)
        {
            var toBe = new RegistryObject[RegKeys.Length + 1];
            int i;
            for (i = 0; i < RegKeys.Length - 1; i++)
            {
                toBe[i] = RegKeys[i];
            }

            toBe[i++] = registryObject;
            RegKeys = toBe;
        }
    }
}