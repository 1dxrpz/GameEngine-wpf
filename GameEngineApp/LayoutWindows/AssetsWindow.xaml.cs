using GameEngineApp.Core;
using GameEngineApp.LayoutWindows.AssetIcons;
using GameEngineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameEngineApp.LayoutWindows
{
	public partial class AssetsWindow : UserControl
	{
		public int WindowWidth { get; set; }
		public int WindowHeight { get; set; }
		public AssetsWindow()
		{
			InitializeComponent();
			ProjectFilesWindow.FolderChanged += ChangeDirectory;
			DataContext = this;
			WindowWidth = (int)MaxWidth;
			WindowHeight = (int)MaxHeight;
			
			AdjustAssets(Navigation.GetAssetsFolder());
			_currentPath = Navigation.GetAssetsFolder();
		}
		string _currentPath = string.Empty;
		private void ChangeDirectory(string path)
		{
			_currentPath = path;
			AdjustAssets(path);
		}
		protected override void OnMouseEnter(MouseEventArgs e)
		{
			AdjustAssets(_currentPath);
		}
		private void AdjustAssets(string path)
		{
			AssetGrid.Children.Clear();
			int i = 0;
			
			foreach (var item in Directory.GetDirectories(path))
			{
				FolderIcon folder = new FolderIcon();
				folder.FolderName = item.Split('\\').Last();
				AssetGrid.Children.Add(folder);
				folder.Tag = item;
				Grid.SetColumn(folder, i);
				i++;
			}
			foreach (var item in Directory.GetFiles(path))
			{
				string type = item.Split('\\').Last().Split('.').Last();
				FileIcon file = new FileIcon(GetFileType(type));
				file.FileName = item.Split('\\').Last().Split('.')[0];
				AssetGrid.Children.Add(file);
				file.Tag = item;
				Grid.SetColumn(file, i);
				i++;
			}
		}
		private FileType GetFileType(string path)
		{
			FileType result;
			switch (path)
			{
				case "cs":
					result = FileType.Component;
					break;
				case "agameobject":
					result = FileType.AnimatedGameObject;
					break;
				case "gameobject":
					result = FileType.GameObject;
					break;
				case "tilemap":
					result = FileType.TileMap;
					break;
				case "animation":
					result = FileType.Animation;
					break;
				case "scene":
					result = FileType.Scene;
					break;
				default:
					result = FileType.File;
					break;
			}
			return result;
		}
	}
}
