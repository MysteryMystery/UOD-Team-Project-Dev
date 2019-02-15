using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace WindowsHardeningSuite.windowshardeningsuite.api.registry.key
{
    public class RegistryObjectBuilder
    {
        private RegistryObject inTheMaking = new RegistryObject();
        
        private Dictionary<RegistryValueKind, Type> pairs = new Dictionary<RegistryValueKind, Type>()
        {
            {RegistryValueKind.Binary, typeof(bool)},
            {RegistryValueKind.String, typeof(string)},
            {RegistryValueKind.DWord, typeof(int)}
        };
        
        internal RegistryObjectBuilder()
        {
            
        }

        public RegistryObjectBuilder SetRegPath(string path)
        {
            inTheMaking.RegPath = path;
            return this;
        }

        public RegistryObjectBuilder SetKeyName(string name)
        {
            inTheMaking.KeyName = name;
            return this;
        }

        public RegistryObjectBuilder SetRegistryValueKind(RegistryValueKind kind)
        {
            inTheMaking.RegistryValueKind = kind;
            return this;
        }

        /*public RegistryObjectBuilder SetValueType(Type type)
        {
            inTheMaking.ValueType = type;
            return this;
        }*/

        public RegistryObject Build()
        {
            if (pairs.ContainsKey(inTheMaking.RegistryValueKind))
                inTheMaking.ValueType = pairs[inTheMaking.RegistryValueKind];
            else
                throw new NotImplementedException("Registry Value type " + inTheMaking.RegistryValueKind.ToString() + " is unsupported!");

            using (RegistryKey regKey = Registry.LocalMachine.OpenSubKey(inTheMaking.RegPath, true)){
                if (regKey == null)
                    throw new NullReferenceException("Registry key not found!");
            };
            
            return inTheMaking;
        }
    }
}