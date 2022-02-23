using GameEngineApp.Core;
using GameEngineApp.LayoutWindows.AssetIcons;
using netCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace GameEngineApp.LayoutWindows
{
	/// <summary>
	/// Interaction logic for SceneWindow.xaml
	/// </summary>
	[Serializable]
	public partial class SceneWindow : UserControl
	{
		public List<GameObject> gameObjects = new List<GameObject>(); 
		public SceneWindow()
		{
			InitializeComponent();
			XmlSerializer xml = new XmlSerializer(typeof(GameObject));
			foreach (var item in Directory.GetFiles($@"{Navigation.MainDirectory}\TestApp\bin\Debug\netcoreapp3.1\gameObjects"))
			{
				var stream = File.OpenRead(item);
				var obj = (GameObject)xml.Deserialize(stream);
				stream.Close();
				Rectangle rect = new Rectangle();
				rect.Width = 200;
				rect.Height = 200;
				rect.Stroke = new SolidColorBrush(Colors.Black);
				rect.Fill = new SolidColorBrush(Colors.Black);
				Canvas.SetLeft(rect, obj.Position.X);
				Canvas.SetTop(rect, obj.Position.Y);
				sceneCanvas.Children.Add(rect);
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			var position = e.GetPosition(sceneCanvas);
			if (e.LeftButton == MouseButtonState.Pressed)
			{

			}
		}
		private void Canvas_Drop(object sender, DragEventArgs e)
		{
			var position = e.GetPosition(sceneCanvas);
			var data = e.Data.GetData(DataFormats.Serializable);
			
			if (data is FileIcon)
			{
				GameObject gameObject = new GameObject()
				{
					Position = new Microsoft.Xna.Framework.Vector2((float)position.X, (float)position.Y)
				};
				Rectangle rect = new Rectangle();
				rect.Stroke = new SolidColorBrush(Colors.Black);
				rect.Fill = new SolidColorBrush(Colors.Black);
				rect.Width = 200;
				rect.Height = 200;
				Canvas.SetLeft(rect, position.X);
				Canvas.SetTop(rect, position.Y);
				sceneCanvas.Children.Add(rect);
				gameObjects.Add(gameObject);
			}
		}
	}
}
