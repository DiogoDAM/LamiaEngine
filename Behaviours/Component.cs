using System;

namespace LamiaEngine 
{
	public abstract class Component : IDisposable
	{
		public Entity Entity;

		public bool IsActive { get; protected set; }
		public bool Disposed { get; protected set; }

		public Component(Entity entity)
		{
			Entity = entity;
		}

		public Component()
		{
			Entity = null;
		}

		public virtual void Start()
		{
			IsActive = false;
		}

		public virtual void Update()
		{
			if(!IsActive || Disposed) return;
		}

		public virtual void Render()
		{
			if(!IsActive || Disposed) return;
		}

		public void OnAdded()
		{
		}

		public void OnRemoved()
		{
			Entity = null;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposable)
		{
			if(disposable)
			{
				if(!Disposed)
				{
					IsActive = false;
					Entity = null;
					Disposed = true;
				}
			}
		}
	}
}
