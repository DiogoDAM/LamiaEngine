using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LamiaEngine
{
	public class Animator
	{
		private Dictionary<string, Animation> _animations;

		public Texture2D Texture;
		public Transform Transform;

		public Animation CurrentAnimation;
		public bool IsRunning { get; private set; }
		public bool IsLooping;
		public Rectangle CurrentFrameBounds { get; private set; }

		public int Count { get { return _animations.Count; } }

		public Animator(Texture2D texture)
		{
			if(texture == null) throw new ArgumentNullException("ERROR! texture in Animator is null");
			Texture = texture;

			_animations = new();
		}

		public void AddAnimation(string animationName, Animation animation)
		{
			if(string.IsNullOrEmpty(animationName))throw new ArgumentNullException("ERROR! animationName in Animator.AddAnimation() is null or empty");

			if(_animations.Count == 0) CurrentAnimation = animation;

			_animations.Add(animationName, animation);
			animation.Texture = Texture;
			animation.Name = animationName;
			animation.Transform = Transform;
		}

		public void RemoveAnimation(string animationName)
		{
			if(string.IsNullOrEmpty(animationName))throw new ArgumentNullException("ERROR! animationName in Animator.RemoveAnimation() is null or empty");

			if(!_animations.ContainsKey(animationName)) throw new KeyNotFoundException("ERROR! animationName not found in Animator.RemoveAnimation()");

			_animations.Remove(animationName);
		}

		public void ChangeAnimation(string animationName)
		{
			if(string.IsNullOrEmpty(animationName))throw new ArgumentNullException("ERROR! animationName in Animator.ChangeAnimation() is null or empty");

			if(!_animations.ContainsKey(animationName)) throw new KeyNotFoundException("ERROR! animationName not found in Animator.ChangeAnimation()");

			CurrentAnimation = _animations[animationName];
		}

		public void Update()
		{
			CurrentAnimation.Update();
		}

		public void Render()
		{
			CurrentAnimation.Render();
		}

		public void Center()
		{
			CurrentAnimation.Center();
		}

		public void Resume()
		{
			CurrentAnimation.Resume();
		}

		public void Restart()
		{
			CurrentAnimation.Restart();
		}

		public void Reset()
		{
			CurrentAnimation.Reset();
		}

		public void Stop()
		{
			CurrentAnimation.Stop();
		}
	}
}
