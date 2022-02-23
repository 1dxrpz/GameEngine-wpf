using AvalonDock.Layout;
using GameEngineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace GameEngineApp.LayoutWindows.AssetIcons
{
	public enum FileType
	{
		File,
		GameObject,
		AnimatedGameObject,
		Component,
		TileMap,
		Animation,
		Scene
	}
	[Serializable]
	public partial class FileIcon : UserControl
	{
		private FileType _fileType;
		public string FileIconPath { get; set; }
		private Visibility _visible = Visibility.Hidden;
		public Visibility Visible
		{
			get => _visible;
			set => _visible = value;
		}
		public string FileName { get; set; }
		private string _workingDir = string.Empty;
		
		public FileIcon(FileType fileType)
		{
			_workingDir = Directory.GetCurrentDirectory();
			_fileType = fileType;
			InitializeComponent();
			DataContext = this;
			FileIconPath = "pack://application:,,,/GameEngineApp;component/Images/";
			switch (fileType)
			{
				case FileType.File:
					FileIconPath += "file";
					break;
				case FileType.GameObject:
					FileIconPath += "gameobject";
					break;
				case FileType.AnimatedGameObject:
					FileIconPath += "animatedgameobject";
					break;
				case FileType.Component:
					FileIconPath += "component";
					break;
				case FileType.TileMap:
					FileIconPath += "tilemap";
					break;
				case FileType.Animation:
					FileIconPath += "animation";
					break;
				case FileType.Scene:
					FileIconPath += "scene";
					break;
				default:
					FileIconPath += "file";
					break;
			}
			FileIconPath += ".png";
		}
		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			base.OnMouseDown(e);
			Visible = Visibility.Visible;
			if (e.ClickCount == 2)
			{
				
				if (_fileType == FileType.Animation)
				{
					LayoutAnchorablePane pane = new LayoutAnchorablePane();
					LayoutAnchorable layout = new LayoutAnchorable();
					layout.Content = new AnimationWindow();
					layout.Title = "Animation: " + FileName;
					pane.FloatingWidth = 600;
					pane.FloatingHeight = 450;
					pane.DockMinWidth = 300;
					pane.DockMinHeight = 300;
					pane.Children.Add(layout);
					MonoGame.WpfCore.BackgroundWorker.MainWindow.LayoutPanel.Children.Add(pane);
					layout.Float();
					layout.IsMaximized = true;
					//layout.ToggleAutoHide();
				}
				if (_fileType == FileType.Component)
				{
					Process cmd = new Process();
					cmd.StartInfo.FileName = "cmd.exe";
					cmd.StartInfo.RedirectStandardInput = true;
					cmd.StartInfo.RedirectStandardOutput = true;
					cmd.StartInfo.CreateNoWindow = true;
					cmd.StartInfo.UseShellExecute = false;
					cmd.Start();

					var path = System.IO.Path.GetFullPath($@"{_workingDir}\..\..\..\TestApp\TestApp.csproj");
					var pathfile = System.IO.Path.GetFullPath($@"{_workingDir}\..\..\..\TestApp\Scripts\Hello.cs");

					cmd.StandardInput.WriteLine($@"start {path}");
					cmd.StandardInput.Flush();
					cmd.StandardInput.WriteLine($@"start {pathfile}");
					cmd.StandardInput.Flush();
					cmd.WaitForExit();
					cmd.StandardInput.Close();

					//Process process = new Process();
					//ProcessStartInfo startInfo = new ProcessStartInfo("devenv.exe", $"/edit {(string)Tag}");
					//ProcessStartInfo startInfo = new ProcessStartInfo("devenv.exe", @"/edit C:\Users\HP\source\repos\GameEngineApp\TestApp\Scripts\Hello.cs");
					//process.StartInfo = startInfo;
					//process.Start();
				}
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				var dragData = new DataObject(DataFormats.Serializable, this);
				
				DragDrop.DoDragDrop(this, dragData, DragDropEffects.Copy);
			}
		}
	}
}
