using GameEngineApp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Windows;

namespace MonoGame.WpfCore
{
	public class BackgroundWorker
	{
		static public Window Window;
		static public MainWindow MainWindow;
		static public GraphicsDevice GraphicsDevice;
		static public WorkCamera Camera;
		static public SpriteBatch Context;
		static public Selection Selection;
		static public Action UpdateEvent;
		static public Action DrawEvent;
		static public Action InitEvent;
		static public void Update() => UpdateEvent?.Invoke();
		static public void Init() => InitEvent?.Invoke();
		static public void Draw() => DrawEvent?.Invoke();
		static public Texture2D GenerateTexture(Color color)
		{
			var texture = new Texture2D(GraphicsDevice, 1, 1);
			var data = new Color[1] { color };
			texture.SetData(data);
			return texture;
		}
	}
}
