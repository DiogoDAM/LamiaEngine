using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace LamiaEngine
{
	public class TextComponent : Component
	{
		public Font Font;
		public string Text;
		public Vector2 Offset;
		public Color Color;
		public SpriteEffects Flip;
		public float Depth;
		public Vector2 Anchor;

		public TextComponent(Entity e) : base(e)
		{
			Color = Color.White;
			Flip = SpriteEffects.None;
			Depth = 0f;
			Anchor = Vector2.Zero;
			Offset = Vector2.Zero;
		}

		public TextComponent() : base()
		{
			Color = Color.White;
			Flip = SpriteEffects.None;
			Depth = 0f;
			Anchor = Vector2.Zero;
			Offset = Vector2.Zero;
		}

		public override void Render()
		{
			Engine.SpriteBatch.DrawString(Font.SpriteFont, Text, Entity.Transform.Position + Offset, Color, Entity.Transform.Rotation, Anchor, Entity.Transform.Scale, Flip, Depth);
		}

		public TextComponent LoadFont(Font font)
		{
			if(font == null) throw new ArgumentNullException("ERROR! font in TextComponent.LoadFont() is null");
			Font = font;

			return this;
		}

		public TextComponent LoadText(string text)
		{
			if(string.IsNullOrEmpty(text)) throw new ArgumentNullException("ERROR! text in TextComponent.LoadText() is null or emtpy");
			Text = text;

			return this;
		}

	}
}
