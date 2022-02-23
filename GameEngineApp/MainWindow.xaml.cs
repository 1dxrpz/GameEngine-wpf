using AvalonDock.Layout;
using AvalonDock.Layout.Serialization;
using GameEngineApp.Core;
using GameEngineApp.LayoutWindows;
using GameEngineApp.Services;
using GameEngineApp.Services.Interfaces;
using MonoGame.WpfCore;
using netCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml.Serialization;

namespace GameEngineApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly IProjectNavigation _navigationService;
		string _workingDir = string.Empty;
		public MainWindow(IProjectNavigation navigation)
		{
			InitializeComponent();
			_navigationService = navigation;
			var test = _navigationService.GetCurrentDirectory();
			//BackgroundWorker.MainWindow = this;
			_workingDir = Directory.GetCurrentDirectory();
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			Application.Current.Shutdown();
		}

		public void AddScene(object sender, RoutedEventArgs e)
		{

		}
		public void BuildProject(object sender, RoutedEventArgs e)
		{
			XmlSerializer xml = new XmlSerializer(typeof(GameObject));
			var i = 0;
			SceneWindow.gameObjects.ForEach((v) =>
			{
				i++;
				var stream = File.OpenWrite($@"{Navigation.MainDirectory}\TestApp\bin\Debug\netcoreapp3.1\gameObjects\object{i}");
				xml.Serialize(stream, v);
				stream.Close();
			});

			Process cmd = new Process();
			cmd.StartInfo.FileName = "cmd.exe";
			cmd.StartInfo.RedirectStandardInput = true;
			cmd.StartInfo.RedirectStandardOutput = true;
			cmd.StartInfo.CreateNoWindow = true;
			cmd.StartInfo.UseShellExecute = false;
			cmd.Start();

			cmd.StandardInput.WriteLine($@"dotnet build {Navigation.MainDirectory}\TestApp\TestApp.csproj");
			cmd.StandardInput.Flush();
			cmd.StandardInput.Close();

			Directory.SetCurrentDirectory($@"{Navigation.MainDirectory}\TestApp\bin\Debug\netcoreapp3.1");
			Process.Start(@"TestApp.exe");

			if (!Directory.Exists($@"{Navigation.MainDirectory}\TestApp\bin\Debug\netcoreapp3.1\gameObjects"))
			{
				Directory.CreateDirectory($@"{Navigation.MainDirectory}\TestApp\bin\Debug\netcoreapp3.1\gameObjects");
			}
			
			
		}
		public void SaveLayout(object sender, RoutedEventArgs e)
		{
			if (dockManager.IsLoaded)
			{
				//string xmlLayoutString = string.Empty;
				//using (StringWriter fs = new StringWriter())
				//{
				//	XmlLayoutSerializer xmlLayout = new XmlLayoutSerializer(this.dockManager);
				//	xmlLayout.Serialize(fs);
				//	xmlLayoutString = fs.ToString();
				//}
				//File.WriteAllText("Layouts/test.txt", xmlLayoutString);
				Stream stream = File.OpenWrite("Layouts/test.txt");
				XmlSerializer xml = new XmlSerializer(dockManager.Layout.GetType());
				//File.WriteAllText("Layouts/test.txt", XamlWriter.Save(dockManager.Layout));
				xml.Serialize(stream, dockManager.Layout);
				stream.Close();
			}
		}
		public void LoadLayout(object sender, RoutedEventArgs e)
		{
			//	if (dockManager.IsLoaded)
			//	{
			//		XmlLayoutSerializer xml = new XmlLayoutSerializer(dockManager);
			//		Stream stream = File.OpenRead("Layouts/test.txt");
			//		xml.Deserialize(stream);
			//		xml.LayoutSerializationCallback += LayoutDeserialized;
			//		stream.Close();
			//	}
		}

		private void LayoutDeserialized(object sender, LayoutSerializationCallbackEventArgs e)
		{
			
		}
		private void MenuItem_Properties(object sender, RoutedEventArgs e)
		{
			AddWindow(new PropertiesWindow(), "Properties");
		}
		private void MenuItem_Assets(object sender, RoutedEventArgs e)
		{
			AddWindow(new AssetsWindow(), "Assets");
		}
		public void AddWindow(Control content, string title = "panel")
		{
			LayoutAnchorablePane pane = new LayoutAnchorablePane();
			LayoutAnchorable layout = new LayoutAnchorable();
			pane.Children.Add(layout);
			layout.Content = content;
			layout.Title = title;
			LayoutPanel.Children.Add(pane);
			layout.ToggleAutoHide();
		}
		public static T FindChild<T>(DependencyObject parent, string childName)
   where T : DependencyObject
		{
			if (parent == null) return null;
			T foundChild = null;
			int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < childrenCount; i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i);
				
				T childType = child as T;
				if (childType == null)
				{
					foundChild = FindChild<T>(child, childName);
					if (foundChild != null) break;
				}
				else if (!string.IsNullOrEmpty(childName))
				{
					var frameworkElement = child as FrameworkElement;
					// If the child's name is set for search
					if (frameworkElement != null && frameworkElement.Name == childName)
					{
						// if the child's name is of the request name
						foundChild = (T)child;
						break;
					}
				}
				else
				{
					foundChild = (T)child;
					break;
				}
			}
			return foundChild;
		}
	}
}
