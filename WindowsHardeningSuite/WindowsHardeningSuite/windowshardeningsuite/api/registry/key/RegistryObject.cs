﻿using Microsoft.Win32;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        /// <summary>
        /// Dictionary of delegates linking CSTypes with the string representations
        /// </summary>
        private Dictionary<String, Func<String, object>> cases = new Dictionary<String, Func<String, object>>
        {
            {"bool", s => s == "true"},
            {"string", s => s},
            {"int", s => int.Parse(s)}
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
        /// String representation of off value
        /// </summary>
        [JsonProperty] public string OffValue { get; set; }
        
        /// <summary>
        /// Windows versions that the key is present in 
        /// </summary>
        [JsonProperty] public string[] WindowsVersions { get; set; }
        
        /// <summary>
        /// The category in which the key belongs to
        /// </summary>
        [JsonProperty] public string Category { get; set; }

        public Type CSType
        {
            get
            {
                switch (ValueType)
                {
                    case "string": 
                        return typeof(String);
                    case "int":
                        return typeof(int);
                    case "bool":
                        return typeof(bool);
                    default:
                        throw new NotImplementedException("Unimplemented type!");
                }
            }
        }

        public RegistryValueKind GetRegistryValueKind()
        {
            Enum.TryParse(ValueKind, out RegistryValueKind toReturn);
            return toReturn;
        }

        /// <summary>
        /// Set the value of the key
        /// </summary>
        /// <param name="value">The value to set</param>
        /// <exception cref="Exception">Thrown when the type of the argument does not match what it should be.</exception>
        /// <exception cref="NotImplementedException">The datatype has not yet been catered for</exception>
        public void SetValue(object value)
        {
            if ((value.GetType() != typeof(string)) && (value.GetType() != CSType))
                throw new Exception("Cannot set key expecting " + CSType.ToString() + " with a " +
                                    value.GetType().ToString());

            object toSet = value;
            if (value is string)
                if (cases.ContainsKey(ValueType))
                    toSet = cases[ValueType].Invoke((string) value);
                else
                    throw new NotImplementedException("Type not supported!");

            Registry.SetValue(Location, ID, toSet, GetRegistryValueKind());
        }

        public Boolean Exists()
        {
			/*var key = Registry.GetValue(Location, ID, null);

			if (key != null)
			{
				return true;
			}
			else
			{
				return false;
			}*/

            return Registry.GetValue(Location, ID, null) != null;
        }
    }
}
