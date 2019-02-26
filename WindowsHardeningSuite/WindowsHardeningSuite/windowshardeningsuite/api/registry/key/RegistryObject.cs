using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WindowsHardeningSuite.windowshardeningsuite.api.registry.key
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RegistryObject
    {   
        [JsonProperty] public string ID { get; set; }
        [JsonProperty] public string Location { get; set; }
        [JsonProperty] public string DisplayName { get; set; }
        [JsonProperty] public string DisplayDescription { get; set; }
        [JsonProperty] public string ValueType { get; set; }
        [JsonProperty] public string ValueKind { get; set; }
        [JsonProperty] public string[] PossibleValues { get; set; }
        [JsonProperty] public string RecommendedValue { get; set; }

        public Type CSType => Type.GetType(ValueType);

        public RegistryValueKind GetRegistryValueKind()
        {
            Enum.TryParse(ValueKind, out RegistryValueKind toReturn);
            return toReturn;
        }

        public void SetValue(object value)
        {
            if (value.GetType() != CSType)
                throw new Exception("Cannot set key expecting " + CSType.ToString() +" with a " + value.GetType().ToString());
            
            //TODO add setting code with subkeys here
        }
    }
}
