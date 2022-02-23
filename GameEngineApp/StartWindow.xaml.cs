using GameEngineApp.Core;
using GameEngineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml.Serialization;

namespace GameEngineApp
{
	/// <summary>
	/// Interaction logic for StartWindow.xaml
	/// </summary>
	public partial class StartWindow : Window
	{
		public ObservableCollection<ProjectInfo> ProjectInfos { get; set; }
		MainWindow _mainWindow;
		public StartWindow(IProjectNavigation navigation)
		{
			InitializeComponent();
			_mainWindow = new MainWindow(navigation);
			ProjectInfos = new ObservableCollection<ProjectInfo>();
			AdjustProjects();
			DataContext = this;
		}
		
		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			if (MainGrid.IsMouseOver && e.LeftButton == MouseButtonState.Pressed)
			{
				DragMove();
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			_mainWindow.Show();
			Close();
		}

		private void CreateNewProject(object sender, RoutedEventArgs e)
		{
			NewProjectDialog newProjectDialiog = new NewProjectDialog();
			if (newProjectDialiog.ShowDialog() == true)
			{
				XmlSerializer xml = new XmlSerializer(typeof(ProjectInfo));
				var path = $@"{Directory.GetCurrentDirectory()}\ProjectsInfo\{newProjectDialiog.ProjectName}";
				var stream = File.Create(path);
				var info = new ProjectInfo()
				{
					Path = newProjectDialiog.Path,
					ProjectName = newProjectDialiog.ProjectName,
					Version = "1.0"
				};
				xml.Serialize(stream, info);
				stream.Close();
				AdjustProjects();
			}
		}
		private void AdjustProjects()
		{
			ProjectInfos.Clear();
			foreach (var proj in Directory.GetFiles($@"{Directory.GetCurrentDirectory()}\\ProjectsInfo"))
			{
				XmlSerializer xml = new XmlSerializer(typeof(ProjectInfo));
				var stream = File.OpenRead(proj);
				var info = (ProjectInfo)xml.Deserialize(stream);
				stream.Close();
				ProjectInfos.Add(info);	
			}
			
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			Application.Current.Shutdown();
		}

		private void Shutdown(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
		private void MaximizeMinimize(object sender, RoutedEventArgs e)
		{
			if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
			{
				Application.Current.MainWindow.WindowState = WindowState.Normal;
				MaximizeMinimizeButton.Source = new BitmapImage(
					new Uri("pack://application:,,,/GameEngineApp;component/Images/maximize.png"));
			} else
			{
				Application.Current.MainWindow.WindowState = WindowState.Maximized;
				MaximizeMinimizeButton.Source = new BitmapImage(
					new Uri("pack://application:,,,/GameEngineApp;component/Images/minimize.png"));
			}
		}
		private void Collapse(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.WindowState = WindowState.Minimized;
		}
	}
}
