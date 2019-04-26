using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsHardeningSuite.windowshardeningsuite.api.config;
using WindowsHardeningSuite.windowshardeningsuite.api.database;
using WindowsHardeningSuite.windowshardeningsuite.api.registry.key;
using WindowsHardeningSuite.windowshardeningsuite.api.database.model;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Navigation;
using System.Diagnostics;

namespace WindowsHardeningSuite.windowshardeningsuite.frontend
{
	public partial class UserInterface
	{
		private static UserInterface _instance;

		private DatabaseWrapper _dbWrapper;
		private RegistryCollection regCollection;

		public static UserInterface GetInstance()
		{
			if (_instance == null)
				_instance = new UserInterface();
			return _instance;
		}

		public DatabaseWrapper GetDatabaseWrapper()
		{
			return _dbWrapper ?? (_dbWrapper = DatabaseWrapper.GetInstance());
		}

		public RegistryCollection GetRegistryCollection()
		{
			return regCollection ??
				(regCollection = ResourceProvider.ProvideJSON<RegistryCollection>(Properties.Resources.keys));
		}

		public static void Init()
		{
			UserInterface userInterface = GetInstance();
			Application userApplication = new Application();
			userApplication.Run(userInterface);
		}

		async void OnSettingToggle(object sender, RoutedEventArgs e)
		{
			string toggleID = ((ToggleSwitch)sender).Name;
			bool toggleEnabled = ((ToggleSwitch)sender).IsChecked.Value;

			if (toggleID == "_RecommendedSettings")
			{
				if (toggleEnabled)
				{
					GetRegistryCollection().SetAllRecommended();
					await this.ShowMessageAsync("Recommended Settings Enabled!", "Little Brother's Recommended Settings have been Enabled.");
				}
				else
				{
					GetRegistryCollection().SetAllOff();
					await this.ShowMessageAsync("Recommended Settings Disabled!", "Little Brother's Recommended Settings have been Disabled.");
				}
			}
			else
			{
				foreach (RegistryObject key in GetRegistryCollection().RegKeys)
				{
					string keyID = key.ID;

					if (toggleID == keyID)
					{
						string keyName = key.DisplayName;
						string newKeyValue;
						string newKeyState;

						if (toggleEnabled)
						{
							newKeyValue = key.RecommendedValue;
							newKeyState = "Enabled";
						}
						else
						{
							newKeyValue = key.OffValue;
							newKeyState = "Disabled";
						}

						TrackedChange newTrackerEntry = new TrackedChange // I feel this makes more sense in the Backend. What happens for the Recommended Batch Toggle Above? UPDATE: Is this still needed at all?
						{
							RegKeyId = keyID,
							SetValue = newKeyValue,
							TimeStamp = new DateTime()
						};

						GetDatabaseWrapper().Insert<TrackedChange>(newTrackerEntry);
						key.SetValue(newKeyValue);

						await this.ShowMessageAsync("'" + keyName + "' Setting " + newKeyState + "!", "The '" + keyName + "' Setting has been " + newKeyState + ".");
						break;
					};
				}
			}
		}

		void OnHyperlinkNavigate(object sender, RequestNavigateEventArgs e)
		{
			Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
			e.Handled = true;
		}
	}
}
