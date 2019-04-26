using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using WindowsHardeningSuite.windowshardeningsuite.api.database.model;
using WindowsHardeningSuite.windowshardeningsuite.api.registry.key;

namespace WindowsHardeningSuite.windowshardeningsuite.frontend
{
	/// <summary>
	/// Interaction logic for UserInterface.xaml
	/// </summary>
	public partial class UserInterface : MetroWindow
	{
		public UserInterface()
		{
			InitializeComponent();
			_RecommendedSettings.IsChecked = false; //key.Exists(); // METHOD CURRENTLY BROKEN!
		}

		private void OnAdvancedSettingsListInit(object sender, EventArgs e)
		{
			bool backgroundAlternative = true;
			SolidColorBrush backgroundColor;
			Dictionary<string, StackPanel> categoryList = new Dictionary<string, StackPanel>();

			foreach (RegistryObject key in GetRegistryCollection().RegKeys)
			{
				StackPanel _StackPanel;

				if (!categoryList.ContainsKey(key.Category))
				{
					Expander _Expander = new Expander();
					_Expander.Header = key.Category;
					_Expander.IsExpanded = true;
					_Expander.Margin = new Thickness(1);

					_StackPanel = new StackPanel();
					_Expander.Content = _StackPanel;

					AdvancedSettingsList.Children.Add(_Expander);
					categoryList.Add(key.Category, _StackPanel);
				}
				else
				{
					_StackPanel = categoryList[key.Category];
				}

				Border _Border = new Border();
				_StackPanel.Children.Add(_Border);
				_Border.BorderThickness = new Thickness(1);
				_Border.Height = 100;

				if (backgroundAlternative)
				{
					backgroundColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE0E0E0"));
				}
				else
				{
					backgroundColor = Brushes.White;
				}
				backgroundAlternative = !backgroundAlternative;
				_Border.Background = backgroundColor;

				Grid _Grid = new Grid();
				_Border.Child = _Grid;

				ToggleSwitch _ToggleSwitch = new ToggleSwitch();
				_Grid.Children.Add(_ToggleSwitch);
				_ToggleSwitch.Header = key.DisplayName;
				_ToggleSwitch.HorizontalAlignment = HorizontalAlignment.Left;
				_ToggleSwitch.Height = 98;
				_ToggleSwitch.VerticalAlignment = VerticalAlignment.Top;
				_ToggleSwitch.Width = 728.688;
				_ToggleSwitch.OffLabel = "";
				_ToggleSwitch.OnLabel = "";
				_ToggleSwitch.Margin = new Thickness(10, 0, 0, 0);
				_ToggleSwitch.Name = key.ID;
				_ToggleSwitch.Click += OnSettingToggle;
				_ToggleSwitch.IsChecked = false; //key.Exists(); // METHOD CURRENTLY BROKEN!

				TextBlock _TextBlock = new TextBlock();
				_Grid.Children.Add(_TextBlock);
				_TextBlock.HorizontalAlignment = HorizontalAlignment.Left;
				_TextBlock.TextWrapping = TextWrapping.Wrap;
				_TextBlock.VerticalAlignment = VerticalAlignment.Top;
				_TextBlock.Margin = new Thickness(10, 26, 0, 0);
				_TextBlock.Width = 651.653;
				_TextBlock.Height = 62;
				_TextBlock.FontStyle = FontStyles.Italic;
				_TextBlock.Text = key.DisplayDescription;
			}
		}
	}
}
