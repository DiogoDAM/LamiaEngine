using System;

namespace LamiaEngine
{
	public class Tween
	{
		private float _start;
		private float _end;
		private float _duration;
		private float _currentTime;
		private Func<float, float> _easingFunction;
		private Action<float> _onUpdate;

		public bool isCompleted { get; private set; }
		public bool IsRunning { get; private set; }

		public Tween(float start, float end, float duration, Func<float, float> easingFunction, Action<float> updateFunction)
		{
			_start = start;
			_end = end;
			_duration = duration;
			_currentTime = 0f;
			_easingFunction = easingFunction;
			_onUpdate = updateFunction;
			IsRunning = true;
		}

		public void Update()
		{
			if(!IsRunning) return;

			_currentTime += Engine.DeltaTime;
			_currentTime = Math.Min(_currentTime, _duration);

			if(_currentTime >= _duration)
			{
				_onUpdate?.Invoke(_end);
				isCompleted = true;
				IsRunning = false;
				return;
			}

			float t = _currentTime / _duration;
			float ease = _easingFunction(t);
			float currentValue = _start + (_end - _start) * ease;

			_onUpdate?.Invoke(currentValue);
		}

		public void Stop()
		{
			IsRunning = false;
		}

		public void Resume()
		{
			IsRunning = true;
		}

		public void Restart()
		{
			isCompleted = false;
			_currentTime = 0f;
		}
	}
}
