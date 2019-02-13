using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsHardeningSuite.windowshardeningsuite.api.registry
{
    public class RegistryEditor
    {
        public void SetKey(string regPath, string keyName, object value, RegistryValueKind registryValueKind)
        {
            using (RegistryKey regKey = Registry.LocalMachine.OpenSubKey(regPath, true)){
                if (regKey == null)
                    throw new Exception("Registry key not found!");
                regKey.SetValue(keyName, value, registryValueKind);
            }
        }
    }
}
