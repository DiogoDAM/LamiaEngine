using Microsoft.Xna.Framework.Graphics;

using System;

namespace LamiaEngine
{
	public sealed class Font : IDisposable
	{
		public SpriteFont SpriteFont { get; private set; }
		public int Size { get; private set; }

        public Font(SpriteFont font, int size)
		{
			if(font == null) throw new System.NullReferenceException("The SpriteFont is null");
			SpriteFont = font;
			Size = size;
		}

		public void Dispose()
		{
			SpriteFont = null;
			GC.SuppressFinalize(this);
		}

    }
}
