using Microsoft.Xna.Framework.Input;

namespace LamiaEngine
{
	public sealed class KeyboardManager
	{
		private KeyboardState _prev;
		private KeyboardState _curr;

		public KeyboardManager()
		{
			_prev = Keyboard.GetState();
			_curr = Keyboard.GetState();
		}

		public void Update()
		{
			_prev = _curr;
			_curr = Keyboard.GetState();
		}

		public bool KeyIsPressed(Keys key)
		{
			return _curr.IsKeyDown(key);
		}

		public bool KeyIsReleased(Keys key)
		{
			return _curr.IsKeyUp(key);
		}
		
		public bool KeyWasPressed(Keys key)
		{
			return _curr.IsKeyDown(key) && _prev.IsKeyUp(key);
		}

		public bool KeyWasReleased(Keys key)
		{
			return _curr.IsKeyUp(key) && _prev.IsKeyDown(key);
		}
	}
}
