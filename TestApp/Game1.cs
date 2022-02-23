using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using netCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace TestApp
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private ScriptManager _scriptManager;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}
		List<GameObject> gameObjects = new List<GameObject>();
		Texture2D texture;
		protected override void Initialize()
		{
			base.Initialize();

			texture = new Texture2D(_graphics.GraphicsDevice, 1, 1);
			var data = new Color[1] { Color.White };
			texture.SetData(data);

			XmlSerializer xml = new XmlSerializer(typeof(GameObject));
			foreach (var item in Directory.GetFiles("gameObjects"))
			{
				var stream = File.OpenRead(item);
				var gameObject = (GameObject)xml.Deserialize(stream);
				gameObjects.Add(gameObject);
				stream.Close();
			}
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_scriptManager = new ScriptManager();
			DxScript.context = _spriteBatch;
			DxScript.GraphicsDevice = GraphicsDevice;
			//_scriptManager.InitializeScripts();
			DxScript.StartEvent?.Invoke();
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();
			DxScript.UpdateEvent?.Invoke();
			gameObjects.ForEach(v =>
			{
				_spriteBatch.Draw(texture, new Rectangle(v.Position.ToPoint(), new Point(200, 200)), Color.White);
			});
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
