using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Scripts
{
	public class Hello : DxScript
	{
		Texture2D texture;
		Vector2 Position = new Vector2();
		public override void Start()
		{
			texture = new Texture2D(GraphicsDevice, 1, 1);
			var data = new Color[1] { Color.White };
			texture.SetData(data);
		}
		public override void Update()
		{
			if (Keyboard.GetState().IsKeyDown(Keys.D))
			{
				Position.X += 20;
			}
			context.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, 100, 100), Color.White);
		}
	}
}