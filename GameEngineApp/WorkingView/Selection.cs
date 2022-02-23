using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.WpfCore
{
	public class Selection
	{
		public List<WorkObject> SelectedObjects = new List<WorkObject>();
		
		public Rectangle Bounds = new Rectangle();
		public Texture2D SelectionBoxTexture;
		public Texture2D SelectedTexture;

		public bool SelectionStarted = false;

		private Point _selStart = new Point();
		private Point _selEnd = new Point();

		public Selection()
		{
			BackgroundWorker.DrawEvent += Draw;
			BackgroundWorker.UpdateEvent += Update;
			SelectionBoxTexture = BackgroundWorker.GenerateTexture(new Color(10, 200, 255, 100));
			SelectedTexture = BackgroundWorker.GenerateTexture(new Color(0, 0, 0, 100));
		}
		MouseState _mouseState;
		KeyboardState _keyboardState;
		private void Update()
		{
			//_mouseState = BackgroundWorker.Mouse.GetState();
			//_keyboardState = BackgroundWorker.Keyboard.GetState();
			//if (SelectedObjects.FindIndex(v => { return v.IsDragging; }) == -1)
			//{
			//	if (_mouseState.LeftButton == ButtonState.Pressed)
			//	{
			//		if (!SelectionStarted)
			//		{
			//			SelectionStarted = true;
			//			_selStart = BackgroundWorker.Camera.ViewportToWorld(_mouseState.Position);
			//			_selEnd = _selStart;
			//		}
			//		else
			//			_selEnd = BackgroundWorker.Camera.ViewportToWorld(_mouseState.Position);
			//	}
			//	if (_mouseState.LeftButton == ButtonState.Released)
			//		ClearSelection();
			//	if (SelectionStarted)
			//	{
			//		Bounds.X = Math.Min(_selEnd.X, _selStart.X);
			//		Bounds.Y = Math.Min(_selEnd.Y, _selStart.Y); ;
			//		Bounds.Width = Math.Max(_selEnd.X, _selStart.X) - Bounds.X;
			//		Bounds.Height = Math.Max(_selEnd.Y, _selStart.Y) - Bounds.Y;
			//	}
			//} else
			//	ClearSelection();
		}
		public void Draw()
		{
			BackgroundWorker.Context.Draw(SelectionBoxTexture, Bounds, Color.White);
		}
		private void ClearSelection()
		{
			Bounds = new Rectangle();
			_selEnd = new Point();
			_selStart = new Point();
			SelectionStarted = false;
		}

	}
}
