using System;

namespace WindowsHardeningSuite.windowshardeningsuite.api.registry.key
{
    [Serializable()]
    public class RegistryCollection
    {
        public RegistryObject[] RegKeys { get; set; } = new RegistryObject[0];

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