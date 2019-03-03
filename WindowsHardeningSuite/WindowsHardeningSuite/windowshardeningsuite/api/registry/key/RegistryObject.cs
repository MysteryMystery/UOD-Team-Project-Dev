using Microsoft.Win32;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework.Constraints;

namespace WindowsHardeningSuite.windowshardeningsuite.api.registry.key
{
    /// <summary>
    /// A representation for a reg key
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class RegistryObject
    {
        private Dictionary<Type, Func<String, object>> cases = new Dictionary<Type, Func<String, object>>
        {
            {typeof(bool), (x) => x == "true"},
            {typeof(string), s => s},
            {typeof(int), s => int.Parse(s)}
        };
        
        /// <summary>
        /// What windows knows the key as
        /// </summary>
        [JsonProperty] public string ID { get; set; }
        /// <summary>
        /// Where the key is
        /// </summary>
        [JsonProperty] public string Location { get; set; }
        /// <summary>
        /// Name to display to the GUI
        /// </summary>
        [JsonProperty] public string DisplayName { get; set; }
        /// <summary>
        /// Description to display to GUI
        /// </summary>
        [JsonProperty] public string DisplayDescription { get; set; }
        /// <summary>
        /// String representation of the class that the CS type should be
        /// </summary>
        [JsonProperty] public string ValueType { get; set; }
        /// <summary>
        /// Value kind enum as string
        /// </summary>
        [JsonProperty] public string ValueKind { get; set; }
        /// <summary>
        /// String representation of the value which the application should set the key as
        /// </summary>
        [JsonProperty] public string RecommendedValue { get; set; }
        /// <summary>
        /// Windows versions that the key is present in 
        /// </summary>
        [JsonProperty] public string[] WindowsVersions { get; set; }

        public Type CSType => Type.GetType(ValueType);

        public RegistryValueKind GetRegistryValueKind()
        {
            Enum.TryParse(ValueKind, out RegistryValueKind toReturn);
            return toReturn;
        }

        public void SetValue(object value)
        {
            if (value.GetType() != CSType)
                throw new Exception("Cannot set key expecting " + CSType.ToString() + " with a " +
                                    value.GetType().ToString());

            //TODO Check if value is legal
            object toSet = value;
            if (value is string)
                if (cases.ContainsKey(CSType))
                    toSet = cases[CSType].Invoke(RecommendedValue);
                else
                    throw new NotImplementedException("Type not supported!");

            Registry.SetValue(Location, ID, Convert.ChangeType(toSet, CSType), GetRegistryValueKind());
        }

        private bool TypeLegal(Type csType, RegistryValueKind registryValueKind)
        {
            if (
                !(csType == typeof(bool) && registryValueKind == RegistryValueKind.Binary) 
                
                )
            {
                return false;
            }

            return true;
        }
    }
}
