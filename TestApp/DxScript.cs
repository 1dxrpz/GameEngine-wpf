using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
	public class DxScript : IDxScript
	{
		static public SpriteBatch context;
		static public GraphicsDevice GraphicsDevice;
		static public Action StartEvent;
		static public Action UpdateEvent;
		public DxScript()
		{
			DxScript.StartEvent += Start;
			DxScript.UpdateEvent += Update;
		}
		public virtual void Start()
		{
			
		}

		public virtual void Update()
		{
			
		}
	}
}
