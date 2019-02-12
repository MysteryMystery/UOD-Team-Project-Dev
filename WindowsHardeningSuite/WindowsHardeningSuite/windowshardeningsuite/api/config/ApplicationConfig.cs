using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsHardeningSuite.windowshardeningsuite.api.config
{
    public class ApplicationConfig : IConfig
    {
        public String Debug { get; set; }

        public Dictionary<String, Dictionary<String, String>> RegKeys {get; set;}

        public ApplicationConfig()
        {
            
        }
    }
}
