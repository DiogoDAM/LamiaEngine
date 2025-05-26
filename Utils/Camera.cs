using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LamiaEngine
{
	public class Camera
	{
		public Transform Transform;

		public Vector2 Position { get { return Transform.Position; } set { Transform.Position = value; } }
		public Vector2 Scale { get { return Transform.Scale; } set { Transform.Scale = value; } }
		public float Rotation { get { return Transform.Rotation; } set { Transform.Rotation = value; } }

		public Viewport Viewport;

		public Camera()
		{
			Transform = new();
			Viewport = new();
		}

		public Camera(Transform trans, int w, int h)
		{
			Transform = trans;
			Viewport = new();
			Viewport.Width = w;
			Viewport.Height = h;
		}

		public Camera(Vector2 pos, int w, int h)
		{
			Transform = new(pos);
			Viewport = new();
			Viewport.Width = w;
			Viewport.Height = h;
		}

		public Matrix GetMatrix()
		{
			return  Matrix.CreateTranslation(-Position.X, -Position.Y, 0f) *
				Matrix.CreateRotationZ(Rotation) *
				Matrix.CreateScale(Scale.X, Scale.Y, 1f) *
				Matrix.CreateTranslation(Viewport.X, Viewport.Y, 0f);
		}

		public Matrix GetMatrixOnCenter()
		{
			return  Matrix.CreateTranslation(-Position.X, -Position.Y, 0f) *
				Matrix.CreateRotationZ(Rotation) *
				Matrix.CreateScale(Scale.X, Scale.Y, 1f) *
				Matrix.CreateTranslation(Viewport.X * .5f, Viewport.Y * .5f, 0f);
		}
	}
}
