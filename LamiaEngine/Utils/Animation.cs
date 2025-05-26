using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LamiaEngine
{
	public class Animation
	{
		public Texture2D Texture;
		public int X, Y, Width, Height;
		public float[] FramesDuration;

		public ushort CurrentFrame;
		public ushort FrameAmount;

		public bool IsLooping;
		public bool IsRunning { get; private set; }

		public string Name;

		public Rectangle CurrentFrameBounds { get { return _framesBounds[CurrentFrame]; } }

		public Color Color;
		public SpriteEffects Flip;
		public Vector2 Anchor;
		public Transform Transform;
		public float Depth;


		private Rectangle[] _framesBounds;
		private float _time;

		public Animation(int x, int y, int w, int h, ushort frameAmount, float frameDuration=.1f, bool loop=false)
		{
			Color = Color.White;
			Flip = SpriteEffects.None;
			Anchor = Vector2.Zero;
			Transform = new();
			Depth = 0f;

			X = x;
			Y = y;
			Width = w;
			Height = h;
			FrameAmount = frameAmount;

			FramesDuration = new float[FrameAmount];
			for(uint i=0; i<FrameAmount; i++)
			{
				FramesDuration[i] = frameDuration;
			}

			_framesBounds = new Rectangle[FrameAmount];
			for(int i=0; i<FrameAmount; i++)
			{
				_framesBounds[i] = new Rectangle(X + (i * Width), Y, Width, Height);
			}

			IsLooping = loop;

			CurrentFrame = 0;
		}

		public Animation(int x, int y, int w, int h, ushort frameAmount, float[] framesDuration, bool loop=false)
		{
			Color = Color.White;
			Flip = SpriteEffects.None;
			Anchor = Vector2.Zero;
			Transform = new();
			Depth = 0f;

			X = x;
			Y = y;
			Width = w;
			Height = h;
			FrameAmount = frameAmount;

			FramesDuration = new float[FrameAmount];
			for(uint i=0; i<FrameAmount; i++)
			{
				FramesDuration[i] = framesDuration[i];
			}

			_framesBounds = new Rectangle[FrameAmount];
			for(int i=0; i<FrameAmount; i++)
			{
				_framesBounds[i] = new Rectangle(X + (i * Width), Y, Width, Height);
			}

			IsLooping = loop;

			CurrentFrame = 0;
		}

		public void Update()
		{
			if(IsRunning)
			{
				_time += Engine.DeltaTime;

				if(_time >= FramesDuration[CurrentFrame])
				{
					_time = 0f;
					CurrentFrame++;

					if(CurrentFrame == FrameAmount)
					{
						if(!IsLooping) IsRunning = false;
						CurrentFrame = 0;
					}
				}
			}
		}

		public void Render()
		{
			Engine.SpriteBatch.Draw(Texture, Transform.Position, CurrentFrameBounds, Color, Transform.Rotation, Anchor, Transform.Scale, Flip, Depth);
		}

		public void Center()
		{
			Anchor = new Vector2(Width * .5f, Height * .5f);
		}

		public void Resume()
		{
			IsRunning = true;
		}

		public void Restart()
		{
			IsRunning = true;
			CurrentFrame = 0;
		}

		public void Reset()
		{
			CurrentFrame = 0;
		}

		public void Stop()
		{
			IsRunning = false;
		}
	}
}
