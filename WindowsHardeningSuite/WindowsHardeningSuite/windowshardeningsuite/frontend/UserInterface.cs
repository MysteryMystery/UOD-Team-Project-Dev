using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsHardeningSuite.windowshardeningsuite.api.config;
using WindowsHardeningSuite.windowshardeningsuite.api.database;
using WindowsHardeningSuite.windowshardeningsuite.api.registry.key;
using MahApps.Metro.Controls;

namespace WindowsHardeningSuite.windowshardeningsuite.frontend
{
	public partial class UserInterface
	{
		private static UserInterface _instance;

		private DatabaseWrapper _dbWrapper;
		private RegistryCollection regCollection;

		public static UserInterface GetInstance() // Do we really need this Method?
		{
			if (_instance == null)
				_instance = new UserInterface();
			return _instance;
		}

		public DatabaseWrapper GetDatabaseWrapper() // Ditto to the above.
		{
			if (_dbWrapper == null)
				_dbWrapper = DatabaseWrapper.GetInstance();
			return _dbWrapper;
		}

		public RegistryCollection GetRegistryCollection()
		{
			if (regCollection == null)
				regCollection = ResourceProvider.ProvideJSON<RegistryCollection>(Properties.Resources.keys);
			return regCollection;
		}

		public static void Init()
		{
			UserInterface userInterface = GetInstance();
			Application userApplication = new Application();
			userApplication.Run(userInterface);
		}

		void OnRecommendedButtonClick(object sender, RoutedEventArgs e)
		{
			bool isEnabled = ((ToggleSwitch)sender).IsChecked.Value;
			
			if (isEnabled)
			{
				GetRegistryCollection().SetAllRecommended();
			}
		}
	}
}
