using System;

namespace LamiaEngine
{
	public static class Easing
	{
		// Linear
		public static float Linear(float t) => t;

		public static float EaseIn(float t) => t * t;
		public static float EaseOut(float t) => t * (2 - t);
		public static float EaseInOut(float t) => t < 0.5 ? 2 * t * t : -1 + (4 - 2 * t) * t;

		// Quadrático
		public static float InQuad(float t) => t * t;
		public static float OutQuad(float t) => 1 - (1 - t) * (1 - t);
		public static float InOutQuad(float t) => 
			t < 0.5 ? 2 * t * t : 1 - MathF.Pow(-2 * t + 2, 2) / 2;
		
		// Cúbico
		public static float InCubic(float t) => t * t * t;
		public static float OutCubic(float t) => 1 - MathF.Pow(1 - t, 3);
		public static float InOutCubic(float t) => 
			t < 0.5 ? 4 * t * t * t : 1 - MathF.Pow(-2 * t + 2, 3) / 2;
		
		// Elástico
		public static float InElastic(float t) => 
			(float)(Math.Sin(13 * Math.PI / 2 * t) * Math.Pow(2, 10 * (t - 1)));
		public static float OutElastic(float t) => 
			(float)(Math.Sin(-13 * Math.PI / 2 * (t + 1)) * Math.Pow(2, -10 * t) + 1);
		
		// Bounce
		public static float OutBounce(float t)
		{
			const float n1 = 7.5625f;
			const float d1 = 2.75f;
			
			if (t < 1 / d1) return n1 * t * t;
			if (t < 2 / d1) return n1 * (t -= 1.5f / d1) * t + 0.75f;
			if (t < 2.5 / d1) return n1 * (t -= 2.25f / d1) * t + 0.9375f;
			return n1 * (t -= 2.625f / d1) * t + 0.984375f;
		}
		
		public static float InBounce(float t) => 1 - OutBounce(1 - t);
	}
}
