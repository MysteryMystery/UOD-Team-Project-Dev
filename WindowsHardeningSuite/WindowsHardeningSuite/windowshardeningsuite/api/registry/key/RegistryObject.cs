using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsHardeningSuite.windowshardeningsuite.api.registry.key
{
    public class RegistryObject
    {
        public string RegPath { get; private set; }
        public string KeyName { get; private set; }
        public object Value { get; private set; }
        public RegistryValueKind RegistryValueKind { get; private set; }

        RegistryObject(string regPath, string keyName, object value, RegistryValueKind registryValueKind)
        {
            RegPath = regPath;
            KeyName = keyName;
            Value = value;
            RegistryValueKind = registryValueKind;
        }
    }
}
