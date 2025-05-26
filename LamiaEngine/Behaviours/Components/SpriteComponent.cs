using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LamiaEngine
{
	public class SpriteComponent : Component
	{
		public Vector2 Offset;
		public Sprite Sprite;
		public SpriteEffects Flip;

		public int Width { get { return Sprite.Width; } }
		public int Height { get { return Sprite.Height; } }

		public SpriteComponent(Entity e) : base(e)
		{
			Sprite = new();
			Sprite.Color = Color.White;
			Flip = SpriteEffects.None;
			Sprite.Depth = 0f;
			Sprite.Anchor = Vector2.Zero;
			Offset = Vector2.Zero;
		}

		public SpriteComponent() : base()
		{
			Sprite = new();
			Sprite.Color = Color.White;
			Flip = SpriteEffects.None;
			Sprite.Depth = 0f;
			Sprite.Anchor = Vector2.Zero;
			Offset = Vector2.Zero;
		}

		public SpriteComponent LoadTexture(Texture2D texture, int x, int y, int w, int h)
		{
			if(texture == null) throw new ArgumentNullException("ERROR! texture in SpriteComponent is null");
			Sprite.LoadTexture(texture, x, y, w, h);

			return this;
		}

		public SpriteComponent LoadTexture(Texture2D texture)
		{
			if(texture == null) throw new ArgumentNullException("ERROR! texture in SpriteComponent is null");
			Sprite.LoadTexture(texture);

			return this;
		}

		public SpriteComponent SetBounds(int x, int y, int w, int h)
		{
			Sprite.SetBounds(x, y, w, h);

			return this;
		}

		public SpriteComponent Center()
		{
			Offset.X = -(Width*.5f);
			Offset.Y = -(Height*.5f);

			return this;
		}

		public override void Render()
		{
			Engine.SpriteBatch.Draw(Sprite.Texture, Entity.Transform.Position + Offset, Sprite.Bounds, Sprite.Color, Entity.Transform.Rotation, Sprite.Anchor, Entity.Transform.Scale, Flip, Sprite.Depth);
		}
	}
}
