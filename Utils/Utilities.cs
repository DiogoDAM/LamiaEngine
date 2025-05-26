using System;
using Microsoft.Xna.Framework;

namespace LamiaEngine
{
	public static class Utilities
	{
		public static Random Random = new Random();

		public static Rectangle GetOverlap(Rectangle rect1, Rectangle rect2)
		{
			int x = Math.Max(rect1.Left, rect2.Right);
			int y = Math.Max(rect1.Top, rect2.Bottom);
			int w = Math.Min(rect1.Right, rect2.Left) - x;
			int h = Math.Min(rect1.Bottom, rect2.Top) - y;

			return new Rectangle(x, y, w, h);
		}

		public static float RandomFloat(float min, float max)
		{
			return (float)(Random.NextDouble() * (max - min) + min);
		}
	}
}
