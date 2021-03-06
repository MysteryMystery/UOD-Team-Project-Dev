using System;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WindowsHardeningSuite.Properties;
using WindowsHardeningSuite.windowshardeningsuite.api.config;
using WindowsHardeningSuite.windowshardeningsuite.api.database;
using WindowsHardeningSuite.windowshardeningsuite.api.registry.key;
using LiteDB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsHardeningSuite.windowshardeningsuite.frontend
{
    public class TestRunner
    {
		[STAThread]
		public static void Main(string[] args)
        {
           
            if (args.Length == 0)
            {
                UserInterface.Init();
            }
            else switch (args[0])
            {
                case "build":
                    CreateAppResource();
                    break;
                case "test":
                {
                    DatabaseWrapper wrapper = DatabaseWrapper.GetInstance();
                    RegistryCollection registryCollection = ResourceProvider.ProvideJSON<RegistryCollection>(Resources.keys);
                    Console.WriteLine("Reg keys length: " + registryCollection.RegKeys.Length);
                    registryCollection.RegKeysAsList.ForEach(key => key.SetValue(key.RecommendedValue));
                    break;
                }
                case "test_gui":
                {
                    UserInterface.Init();
                    break;
                }
            }
        }

        private static void LoadAppResource()
        {
            RegistryCollection registryCollection = ResourceProvider.ProvideJSON<RegistryCollection>(Resources.keys);
            foreach (var key in registryCollection.RegKeys)
            {
                Console.WriteLine(key.DisplayName);
            }
        } 

        private static void CreateAppResource()
        {
            /*
            RegistryObject registryObject = new RegistryObject();
            registryObject.ValueKind = "Value Kind";                    // Registry Data Type (DWord | String).
            registryObject.ValueType = "Value Type";                    // C# Data Type (System.Boolean | System.Int | System.String).
            registryObject.WindowsVersions = new string[]
            {
                "Value 1",												// Minimum Applicable Windows Build (NEEDS CHANGING!).
                "Value 2"
            };
            registryObject.Location = "location";                       // Registry Key Filepath.
            registryObject.ID = "Test Key";								// Registry Key Name.
            registryObject.DisplayName = "Test Display Key";            // Registry Key Friendly Name.
            registryObject.RecommendedValue = "Value 1";				// Registry Key's New Value?
            registryObject.DisplayDescription = "Test display desc";	// Registry Key Friendly Description.
            */

            RegistryCollection registryCollection = new RegistryCollection();
            registryCollection.KeyCategories = new[]
            {
                new KeyCategory
                {
                    Name = "Misc."
                },
                new KeyCategory
                {
                    Name = "Telemetry"
                }
            };

            RegistryObject registryObject = new RegistryObject();
            registryObject.ValueKind = "DWord";
            registryObject.ValueType = "System.Bool";
            registryObject.WindowsVersions = new string[]
            {
                "Windows 10",
                "10.0.10240"
            };
            registryObject.Location = "HKLM\\Software\\Policies\\Microsoft\\Windows\\DataCollection";
            registryObject.ID = "AllowTelemetry";
            registryObject.DisplayName = "Allow Telemetry";
            registryObject.RecommendedValue = "false";
            registryObject.DisplayDescription = "This policy setting determines the amount of Windows diagnostic data sent to Microsoft. A value of 0 (Security) will send minimal data to Microsoft to keep Windows secure. Windows security components such as Malicious Software Removal Tool (MSRT) and Windows Defender may send data to Microsoft at this level if they are enabled. Setting a value of 0 applies to Enterprise, EDU, IoT and Server devices only. Setting a value of 0 for other devices is equivalent to setting a value of 1.  A value of 1 (Basic) sends the same data as a value of 0, plus a very limited amount of diagnostic data such as basic device info, quality-related data, and app compatibility info. Note that setting values of 0 or 1 will degrade certain experiences on the device.  A value of 2 (Enhanced) sends the same data as a value of 1, plus additional data such as how Windows, Windows Server, System Center, and apps are used, how they perform, and advanced reliability data.  A value of 3 (Full) sends the same data as a value of 2, plus advanced diagnostics data used to diagnose and fix problems with devices, which can include the files and content that may have caused a problem with the device.  Windows 10 diagnostics data settings applies to the Windows operating system and apps included with Windows. This setting does not apply to third party apps running on Windows 10.  If you disable or do not configure this policy setting, users can configure the Telemetry level in Settings.";
            registryObject.Category = "Telemetry";
            
            registryCollection.Add(registryObject);
            
			registryObject = new RegistryObject();
			registryObject.ValueKind = "DWord";
			registryObject.ValueType = "System.Bool";
			registryObject.WindowsVersions = new string[]
			{
				"Windows XP",
				"5.1.2600"
			};
			registryObject.Location = "HKLM\\Software\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU";
			registryObject.ID = "NoAutoUpdate";
            registryObject.DisplayName = "Configure Automatic Updates";
			registryObject.RecommendedValue = "false";
			registryObject.DisplayDescription = "Specifies whether this computer will receive security updates and other important downloads through the Windows automatic updating service.  Note: This policy does not apply to Windows RT.  This setting lets you specify whether automatic updates are enabled on this computer. If the service is enabled, you must select one of the four options in the Group Policy Setting:  2 = Notify before downloading and installing any updates.  When Windows finds updates that apply to this computer, users will be notified that updates are ready to be downloaded. After going to Windows Update, users can download and install any available updates.  3 = (Default setting) Download the updates automatically and notify when they are ready to be installed  Windows finds updates that apply to the computer and downloads them in the background (the user is not notified or interrupted during this process). When the downloads are complete, users will be notified that they are ready to install. After going to Windows Update, users can install them.  4 = Automatically download updates and install them on the schedule specified below.  Specify the schedule using the options in the Group Policy Setting. If no schedule is specified, the default schedule for all installations will be every day at 3:00 AM. If any updates require a restart to complete the installation, Windows will restart the computer automatically. (If a user is signed in to the computer when Windows is ready to restart, the user will be notified and given the option to delay the restart.)  On Windows 8 and later, you can set updates to install during automatic maintenance instead of a specific schedule. Automatic maintenance will install updates when the computer is not in use, and avoid doing so when the computer is running on battery power. If automatic maintenance is unable to install updates for 2 days, Windows Update will install updates right away. Users will then be notified about an upcoming restart, and that restart will only take place if there is no potential for accidental data loss.  Automatic maintenance can be further configured by using Group Policy settings here: Computer Configuration->Administrative Templates->Windows Components->Maintenance Scheduler  5 = Allow local administrators to select the configuration mode that Automatic Updates should notify and install updates.  With this option, local administrators will be allowed to use the Windows Update control panel to select a configuration option of their choice. Local administrators will not be allowed to disable the configuration for Automatic Updates.  If the status for this policy is set to Disabled, any updates that are available on Windows Update must be downloaded and installed manually. To do this, search for Windows Update using Start.  If the status is set to Not Configured, use of Automatic Updates is not specified at the Group Policy level. However, an administrator can still configure Automatic Updates through Control Panel.";
            registryObject.Category = "Misc.";
			
            registryCollection.Add(registryObject);
            
            Console.WriteLine(JToken.Parse(JsonConvert.SerializeObject(registryCollection)).ToString(Formatting.Indented));
        }
    }
}