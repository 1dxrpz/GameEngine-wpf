using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGame.WpfCore
{
	public class WorkObject
	{
		public static Action<WorkObject> ObjectSelected;
		public Vector2 Position = new Vector2();
		public Texture2D Texture;
		public bool Selected = false;
		public bool IsDragging = false;

		public int Width = 32;
		public int Height = 32;

		public WorkRenderer Renderer;

		private bool _clicked = false;
		private bool _mouseDown = false;

		public bool IsHover
		{
			get => true;
				//BackgroundWorker.Camera.ViewportToWorld(BackgroundWorker.Mouse.GetState().Position).X > Position.X &&
				//BackgroundWorker.Camera.ViewportToWorld(BackgroundWorker.Mouse.GetState().Position).Y > Position.Y &&
				//BackgroundWorker.Camera.ViewportToWorld(BackgroundWorker.Mouse.GetState().Position).X < (Position.X + Width) &&
				//BackgroundWorker.Camera.ViewportToWorld(BackgroundWorker.Mouse.GetState().Position).Y < (Position.Y + Height);
		}
		public bool OnClick
		{
			get
			{
				ObjectSelected?.Invoke(this);
				return IsHover && _clicked;
			}
		}

		MouseState _mouseState;
		KeyboardState _keyboardState;
		public WorkObject(Texture2D texture)
		{
			Texture = texture;
			//Texture = BackgroundWorker.GenerateTexture(Color.Red);
			Renderer = new WorkRenderer(this);
			BackgroundWorker.DrawEvent += Renderer.Draw;
			BackgroundWorker.UpdateEvent += Update;
		}
		private void Update()
		{
			//_mouseState = BackgroundWorker.Mouse.GetState();
			//_keyboardState = BackgroundWorker.Keyboard.GetState();
			//if (_mouseState.LeftButton == ButtonState.Pressed && _clicked)
			//	_clicked = false;
			//if (_mouseState.LeftButton == ButtonState.Pressed && !_clicked && !_mouseDown)
			//{
			//	_mouseDown = true;
			//	_clicked = true;
			//}
			//if (_mouseState.LeftButton == ButtonState.Released)
			//	_mouseDown = false;

			//if (OnClick && _mouseDown)
			//	IsDragging = true;

			//if (_mouseState.LeftButton == ButtonState.Released)
			//{
			//	_mouseDown = false;
			//	IsDragging = false;
			//}
			//if (IsDragging)
			//{
			//	BackgroundWorker.Selection.SelectedObjects.ForEach(v =>
			//	{
			//		v.Position -= BackgroundWorker.Camera.MouseDelta;
			//	});
			//}
			//if (
			//	BackgroundWorker.Selection.Bounds.Intersects(
			//		new Rectangle(Position.ToPoint(), new Point(Width, Height)))
			//	|| _keyboardState.IsKeyDown(Keys.LeftControl) && OnClick)
			//{
			//	Selected = true;
			//	if (!BackgroundWorker.Selection.SelectedObjects.Contains(this))
			//	{
			//		BackgroundWorker.Selection.SelectedObjects.Add(this);
			//	}
			//} else
			//{
			//	if (BackgroundWorker.Selection.SelectionStarted)
			//	{
			//		Selected = false;
			//		if (BackgroundWorker.Selection.SelectedObjects.Contains(this))
			//		{
			//			BackgroundWorker.Selection.SelectedObjects.Remove(this);
			//		}
			//	}
			//}
			//if (Selected && _keyboardState.IsKeyDown(Keys.D))
			//{
			//	Selected = false;
			//	Delete();
			//}
		}

		public void Delete()
		{
			BackgroundWorker.DrawEvent -= Renderer.Draw;
			BackgroundWorker.UpdateEvent -= Update;
		}
	}
}
