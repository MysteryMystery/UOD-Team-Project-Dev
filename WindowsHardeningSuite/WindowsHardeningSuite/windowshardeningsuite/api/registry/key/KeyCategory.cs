using System;
using Newtonsoft.Json;

namespace WindowsHardeningSuite.windowshardeningsuite.api.registry.key
{
    [JsonObject(MemberSerialization.OptIn)]
    public class KeyCategory
    {
        [JsonProperty]
        public String Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(KeyCategory))
                return false;
            KeyCategory compared = (KeyCategory) obj;
            return compared.Name.ToLower() == Name.ToLower();
        }
    }
}