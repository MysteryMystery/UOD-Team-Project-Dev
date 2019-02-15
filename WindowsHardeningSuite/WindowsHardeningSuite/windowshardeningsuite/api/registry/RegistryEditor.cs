using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsHardeningSuite.windowshardeningsuite.api.registry.key;

namespace WindowsHardeningSuite.windowshardeningsuite.api.registry
{
    public class RegistryEditor
    {
        public void SetKey(RegistryObject registryObject)
        {
            using (RegistryKey regKey = Registry.LocalMachine.OpenSubKey(registryObject.RegPath, true)){
                if (regKey == null)
                    throw new Exception("Registry key not found!");
                regKey.SetValue(registryObject.KeyName, registryObject.Value, registryObject.RegistryValueKind);
            }
        }
    }
}
