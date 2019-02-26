using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsHardeningSuite.windowshardeningsuite.api.config
{
    /// <summary>
    /// Loads resources using utility methods.
    /// </summary>
    public class ResourceProvider
    {
        public static T ProvideJSON<T>(byte[] resource) where T : new()
        {
            string jsonString = System.Text.Encoding.UTF8.GetString(resource);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}