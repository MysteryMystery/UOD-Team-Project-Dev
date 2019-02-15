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
        public string RegPath { get; internal set; }
        public string KeyName { get; internal set; }
        public RegistryValueKind RegistryValueKind { get; internal set; }

        public Type ValueType { get; internal set; } = typeof(object);
    
        internal RegistryObject(string regPath, string keyName, object value, RegistryValueKind registryValueKind)
        {
            RegPath = regPath;
            KeyName = keyName;
            RegistryValueKind = registryValueKind;
        }
        
        internal RegistryObject()
        {
        }
    
        public void Set(ValueType value)
        {
            using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(RegPath, true))
            {
                registryKey.SetValue(KeyName, value, RegistryValueKind);
            }
        }
        
        public static RegistryObjectBuilder Builder() => new RegistryObjectBuilder();
    }
}
