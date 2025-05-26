using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace LamiaEngine
{
	public static class Input
	{
		public static MouseManager Mouse = new MouseManager();
		public static KeyboardManager Keyboard = new KeyboardManager();


		private static Dictionary<string, List<Func<bool>>> _inputActions = new();

		public static void Update()
		{
			Mouse.Update();
			Keyboard.Update();
		}

		public static void AddAction(string actionName)
		{
			if(String.IsNullOrEmpty(actionName)) throw new ArgumentNullException($"The action {actionName} is null or empty");

			_inputActions.Add(actionName, new List<Func<bool>>());
		}
		
		public static void RemoveAction(string actionName)
		{
			if(String.IsNullOrEmpty(actionName)) throw new ArgumentNullException($"The action {actionName} is null or empty");

			if(!_inputActions.ContainsKey(actionName)) return;

			_inputActions.Remove(actionName);
		}

		public static void AddKeyboardInputToAction(string actionName, Func<Keys, bool> method, Keys key)
		{
			if(String.IsNullOrEmpty(actionName)) throw new ArgumentNullException($"The action {actionName} is null or empty");

			if(!_inputActions.ContainsKey(actionName)) throw new KeyNotFoundException($"The action {actionName} does not exist");

			_inputActions[actionName].Add(() => method(key));
		}

		public static void AddMouseInputToAction(string actionName, Func<int, bool> method, int button)
		{
			if(String.IsNullOrEmpty(actionName)) throw new ArgumentNullException($"The action {actionName} is null or empty");

			_inputActions[actionName].Add(() => method(button));
		}

		public static bool CheckAction(string actionName)
		{
			if(String.IsNullOrEmpty(actionName)) throw new ArgumentNullException($"The action {actionName} is null or empty");

			if(!_inputActions.ContainsKey(actionName)) throw new KeyNotFoundException($"The action {actionName} does not exist");

			foreach(var input in _inputActions[actionName])
			{
				if(input()) return true;
			}

			return false;
		}

		public static bool CheckAllActions(string actionName)
		{
			if(String.IsNullOrEmpty(actionName)) throw new ArgumentNullException($"The action {actionName} is null or empty");

			if(!_inputActions.ContainsKey(actionName)) throw new KeyNotFoundException($"The action {actionName} does not exist");

			foreach(var input in _inputActions[actionName])
			{
				if(!input()) return false;
			}

			return true; 
		}

		public static void Clear()
		{
			_inputActions.Clear();
		}


	}
}
