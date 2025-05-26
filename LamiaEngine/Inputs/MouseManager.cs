using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LamiaEngine
{
	public sealed class MouseManager
	{
		private MouseState _prev;
		private MouseState _curr;

		public Vector2 GlobalPosition { get { 
			if(_camera != null)
		 	{
		 		return Vector2.Transform(_curr.Position.ToVector2(), Matrix.Invert(_camera.GetMatrix())); 
		 	}
		 	else
		 	{
		 	    return _curr.Position.ToVector2();
		 	}
		} }

		private Camera _camera;

		public Vector2 Position { get { return _curr.Position.ToVector2(); } }

		public float WheelValue { get { return _curr.ScrollWheelValue/120; } }
		// private Camera _camera;

		public MouseManager()
		{
			_prev = Mouse.GetState();
			_curr = Mouse.GetState();
		}

		public void Update()
		{
			_prev = _curr;
			_curr = Mouse.GetState();
		}

		public void SetCamera(Camera cam)
		{
		 	_camera = cam;
		}

		public bool WheelWasMoved()
		{
			return _curr.ScrollWheelValue - _prev.ScrollWheelValue != 0;
		}

		// -1 -> down
		// 1 -> up
		public int WheelDirectionMovement()
		{
			return _curr.ScrollWheelValue/120 - _prev.ScrollWheelValue/120;
		}

		public bool ButtonIsPressed(int button)
		{
			 switch(button)
			{
				case 0: return ButtonIsPressed(_curr.LeftButton);
				case 1: return ButtonIsPressed(_curr.RightButton);
				case 2: return ButtonIsPressed(_curr.MiddleButton);
				default: throw new System.Exception($"The value not correspond for any mouse button ({button})");
			}
		}

		public bool ButtonIsReleased(int button)
		{
			 switch(button)
			{
				case 0: return ButtonIsReleased(_curr.LeftButton);
				case 1: return ButtonIsReleased(_curr.RightButton);
				case 2: return ButtonIsReleased(_curr.MiddleButton);
				default: throw new System.Exception($"The value not correspond for any mouse button ({button})");
			}
		}

		public bool ButtonWasPressed(int button)
		{
			 switch(button)
			{
				case 0: return ButtonWasPressed(_curr.LeftButton, _prev.LeftButton);
				case 1: return ButtonWasPressed(_curr.RightButton, _prev.RightButton);
				case 2: return ButtonWasPressed(_curr.MiddleButton, _prev.MiddleButton);
				default: throw new System.Exception($"The value not correspond for any mouse button ({button})");
			}
		}

		public bool ButtonWasReleased(int button)
		{
			 switch(button)
			{
				case 0: return ButtonWasReleased(_curr.LeftButton, _prev.LeftButton);
				case 1: return ButtonWasReleased(_curr.RightButton, _prev.RightButton);
				case 2: return ButtonWasReleased(_curr.MiddleButton, _prev.MiddleButton);
				default: throw new System.Exception($"The value not correspond for any mouse button ({button})");
			}
		}

		private bool ButtonIsPressed(ButtonState state)
		{
			return state == ButtonState.Pressed;
		}

		private bool ButtonIsReleased(ButtonState state)
		{
			return state == ButtonState.Released;
		}

		private bool ButtonWasPressed(ButtonState currState, ButtonState prevState)
		{
			return currState == ButtonState.Pressed && prevState == ButtonState.Released;
		}

		private bool ButtonWasReleased(ButtonState currState, ButtonState prevState)
		{
			return currState == ButtonState.Released && prevState == ButtonState.Pressed;
		}
	}
}
