using Microsoft.Xna.Framework;

namespace MonoGame.WpfCore
{
	public class WorkRenderer
	{
		public WorkObject Parent;

		public WorkRenderer(WorkObject parent)
		{
			Parent = parent;
		}
		public void Draw()
		{
			BackgroundWorker.Context.Draw(
				Parent.Texture,
				new Rectangle(
					Parent.Position.ToPoint(),
					new Point(Parent.Width, Parent.Height)
				),
				Color.White);
			if (Parent.Selected)
			{
				BackgroundWorker.Context.Draw(BackgroundWorker.Selection.SelectedTexture,
					new Rectangle(
					(Parent.Position - new Vector2(0, 0)).ToPoint(),
					new Point(Parent.Width + 0, Parent.Height + 0)
				),
				Color.White);
			}
		}
	}
}
