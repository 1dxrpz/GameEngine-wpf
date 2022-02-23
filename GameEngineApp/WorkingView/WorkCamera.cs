using Microsoft.Xna.Framework;

namespace MonoGame.WpfCore
{
	public class WorkCamera
	{
		public Vector2 Position = new Vector2();
		public Vector2 MouseDelta = new Vector2();
		private Matrix _transform;
		public float Rotation = 0f;
		public float Zoom = 1f;
		public float ViewportWidth = 0;
		public float ViewportHeight = 0;
		
		public Matrix GetTransform()
		{
			_transform =
				Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
				Matrix.CreateRotationZ(Rotation) *
				Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
			return _transform;
		}
		public Point ViewportToWorld(Point vector)
		{
			return ((vector.ToVector2() / Zoom + Position)).ToPoint();
		}
	}
}
