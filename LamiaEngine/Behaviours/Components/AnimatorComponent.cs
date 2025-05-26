using System;

using Microsoft.Xna.Framework.Graphics;

namespace LamiaEngine
{
	public class AnimatorComponent : Component
	{
		private Animator _anim;

		public AnimatorComponent() : base()
		{
		}

		public AnimatorComponent SetTexture(Texture2D texture)
		{
			if(texture == null) throw new ArgumentNullException("ERROR! texture in AnimatorComponent.SetTexture() is null");
			_anim = new(texture);

			return this;
		}

		public override void Update()
		{
			base.Update();

			_anim.Update();
		}

		public override void Render()
		{
			base.Render();

			_anim.Render();
		}

		public AnimatorComponent AddAnimation(string animationName, Animation anim)
		{
			_anim.AddAnimation(animationName, anim);

			return this;
		}

		public AnimatorComponent RemoveAnimation(string animationName)
		{
			_anim.RemoveAnimation(animationName);

			return this;
		}

		public AnimatorComponent ChangeAnimation(string animationName)
		{
			_anim.ChangeAnimation(animationName);

			return this;
		}

		public AnimatorComponent SetDefaultValues(Transform trans)
		{
			_anim.Transform = trans;

			return this;
		}

		public Animation GetCurrentAnimation()
		{
			return _anim.CurrentAnimation;
		}

		public void Center()
		{
			_anim.Center();
		}

		public void Resume()
		{
			_anim.Resume();
		}

		public void Restart()
		{
			_anim.Restart();
		}

		public void Reset()
		{
			_anim.Reset();
		}

		public void Stop()
		{
			_anim.Stop();
		}
	}
}
