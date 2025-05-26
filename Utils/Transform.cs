using Microsoft.Xna.Framework;

namespace LamiaEngine
{
	public sealed class Transform
	{
		public Vector2 Position;
		public Vector2 Scale;
		public float Rotation;

		public Transform Parent;

		public Vector2 GlobalPosition
		{
			get { return (Parent != null) ? Position + Parent.GlobalPosition : Position ; }
		}

		public Transform()
		{
			Position = Vector2.Zero;
			Scale = Vector2.One;
			Rotation = 0f;
		}

		public Transform(Vector2 pos)
		{
			Position = pos;
			Scale = Vector2.One;
			Rotation = 0f;
		}

		public Transform(Vector2 pos, Vector2 scale, float rot)
		{
			Position = pos;
			Scale = scale;
			Rotation = rot;
		}

		public Transform(Transform parent)
		{
			Parent = parent;
			Scale = Vector2.One;
		}

		public Transform(Transform parent, Vector2 pos)
		{
			Parent = parent;
			Position = pos;
			Scale = Vector2.One;
			Rotation = 0f;
		}

		public Matrix GetMatrix()
		{
			return Matrix.CreateScale(Scale.X, Scale.Y, 1f) *
				Matrix.CreateRotationZ(Rotation) *
				Matrix.CreateTranslation(Position.X, Position.Y, 0f);
		}

		public Matrix GetGlobalMatrix()
		{
			if(Parent != null)
			{
				return GetMatrix() * Parent.GetMatrix();
			}
			return GetMatrix();
		}

		public float Distance(Vector2 target)
		{
			return Vector2.Distance(Position, target);
		}

		public static Vector2 MoveTowards(Vector2 start, Vector2 target, float speed)
		{
			Vector2 direction = target - start;

			if(direction.Length() <= speed)
			{
				return target;
			}

			direction.Normalize();
			return start + direction * speed;
		}

		public void MoveTowards(Vector2 target, float speed)
		{
			Position = Transform.MoveTowards(Position, target, speed);
		}

		public void Translate(Vector2 pos)
		{
			Position += pos;
		}

		public void Rotate(float rot)
		{
			Rotation += rot;
		}

		public void LookAt(Vector2 target)
		{
			Vector2 d = target - Position;
			Rotation = (float) System.Math.Atan2(d.Y, d.X);
		}

		public void ScaleBy(Vector2 scale)
		{
			Scale *= scale;
		}

		public void ScaleByScalar(float scalar)
		{
			Scale *= scalar;
		}

		public override string ToString()
		{
			return $"(Position: {Position}, Scale: {Scale}, Rotation: {Rotation})";
		}
	}
}
