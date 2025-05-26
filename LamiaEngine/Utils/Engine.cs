using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LamiaEngine
{
	public static class Engine
	{
		public static SpriteBatch SpriteBatch { get; private set; }
		public static AssetsManager Assets { get; private set; }
		public static float DeltaTime { get; private set; }
		public static float Fps { get; private set; }
		public static float FrameCount { get; private set; }
		public static Font DefaultFont;
		public static int WindowWidth=720, WindowHeight=480;
		public static bool IsFullScreen;

		public static int TileSize;

		private static GraphicsDeviceManager _graphics;
		private static float _elapsedGameTime;



		public static void Load(GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice, ContentManager content)
		{
			SpriteBatch = new SpriteBatch(graphicsDevice);
			_graphics = graphics;
			Assets = new(content);
			Drawer.Initialize(graphicsDevice);
		}

		public static void ResizeWindow(int width, int height)
		{
			WindowWidth = width;
			WindowHeight = height;
			_graphics.PreferredBackBufferWidth = WindowWidth;
			_graphics.PreferredBackBufferHeight = WindowHeight;
			_graphics.ApplyChanges();
		}

		public static void SetFullscreen(bool value)
		{
			_graphics.IsFullScreen = value;
			IsFullScreen = value;

			_graphics.ApplyChanges();
		}

		public static void Update(GameTime gameTime)
		{
			DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			_elapsedGameTime += DeltaTime;
			FrameCount++;

			if(_elapsedGameTime >= 1f)
			{
				Fps = FrameCount;
				FrameCount = 0;
				_elapsedGameTime -= 1;
			}
		}

	}
}
