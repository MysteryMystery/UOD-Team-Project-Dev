using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsHardeningSuite.windowshardeningsuite.api.config
{
    public class ApplicationConfig : Config
    {
        public Boolean Debug { get; set; } = false;

        public Dictionary<String, Dictionary<String, String>> RegKeys { get; set; } = new Dictionary<string, Dictionary<string, string>>();

        public ApplicationConfig()
        {
            
        }
    }
}
