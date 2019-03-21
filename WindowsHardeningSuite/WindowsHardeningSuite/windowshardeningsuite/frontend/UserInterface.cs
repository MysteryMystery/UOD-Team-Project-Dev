using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsHardeningSuite.windowshardeningsuite.api.config;
using WindowsHardeningSuite.windowshardeningsuite.api.database;
using WindowsHardeningSuite.windowshardeningsuite.api.registry.key;

namespace WindowsHardeningSuite.windowshardeningsuite.frontend
{
	/// <summary>
	/// Change the class name to the main class...
	/// </summary>
	/*
    class ExampleStart
    {
        private static ExampleStart _instance;

        private DatabaseWrapper _dbWrapper;
        private RegistryCollection _registryCollection;

        public static ExampleStart GetInstance()
        {
            if (_instance == null)
                _instance = new ExampleStart();
            return _instance;
        }

        public DatabaseWrapper GetDatabaseWrapper()
        {
            if (_dbWrapper == null)
                _dbWrapper = DatabaseWrapper.GetInstance();
            return _dbWrapper;
        }

        public RegistryCollection GetRegistryCollection()
        {
            if (_registryCollection == null)
                _registryCollection = ResourceProvider.ProvideJSON<RegistryCollection>(Properties.Resources.keys); 
            return _registryCollection;
        }
    }
	*/

	public partial class UserInterface
	{
		public static void Init()
		{
			DatabaseWrapper dbWrapper = DatabaseWrapper.GetInstance();
			RegistryCollection regCollection = ResourceProvider.ProvideJSON<RegistryCollection>(Properties.Resources.keys);

			UserInterface userInterface = new UserInterface();
			Application userApplication = new Application();
			userApplication.Run(userInterface);
		}
	}
}
