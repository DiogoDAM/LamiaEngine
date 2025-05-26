using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LamiaEngine
{
	public class Sprite
	{
		public Texture2D Texture;
		public Rectangle Bounds { get { return new Rectangle(X, Y, Width, Height); } }
		public Color Color;
		public float Depth;
		public Vector2 Anchor;

		public int X, Y, Width, Height;

		public Sprite() : base()
		{
			Color = Color.White;
			Depth = 0f;
			Anchor = Vector2.Zero;
		}

		public void LoadTexture(Texture2D texture, int x, int y, int w, int h)
		{
			if(texture == null) throw new ArgumentNullException("ERROR! texture in SpriteComponent is null");
			Texture = texture;
			X = x;
			Y = y;
			Width = w;
			Height = h;
		}

		public void LoadTexture(Texture2D texture)
		{
			if(texture == null) throw new ArgumentNullException("ERROR! texture in SpriteComponent is null");
			Texture = texture;
			X = 0;
			Y = 0;
			Width = texture.Width;
			Height = texture.Height;
		}

		public void SetBounds(int x, int y, int w, int h)
		{
			X = x;
			Y = y;
			Width = w;
			Height = h;
		}

		public static Sprite CreateSprite(Texture2D texture, int x, int y, int w, int h)
		{
			Sprite sprite = new Sprite();
			sprite.LoadTexture(texture, x, y, w, h);
			return sprite;
		}
	}
}
