using System;
using System.Collections.Generic;

namespace LamiaEngine
{
	public abstract class Scene : IDisposable 
	{
		protected Dictionary<string, EntityList> _sceneLayers;

		public bool IsActive { get; protected set; }
		public bool Disposed { get; protected set; }

		public string Name { get; protected set; } = "NoName";

		public Camera Camera;

		public Scene()
		{
			_sceneLayers = new();
			_sceneLayers.Add("Objs", new EntityList(this));
		}

		public virtual void Start()
		{
			IsActive = true;

			Input.Mouse.SetCamera(Camera);

			foreach(var pair in _sceneLayers)
			{
				pair.Value.Start();
			}
		}

		public virtual void Update()
		{
			if(!IsActive || Disposed) return;

			foreach(var pair in _sceneLayers)
			{
				pair.Value.Update();
			}
		}

		public virtual void Render()
		{
			if(!IsActive || Disposed) return;

			foreach(var pair in _sceneLayers)
			{
				pair.Value.Render();
			}
		}

		public virtual void RenderUi()
		{
			if(!IsActive || Disposed) return;
		}

		public void Active()
		{
			Start();
		}

		public void Desactive()
		{
		}

		public void OnAdded()
		{
		}

		public void OnRemoved()
		{
		}

		public void AddEntity(Entity e, string layer="Objs")
		{
			if(e == null) throw new ArgumentNullException("ERROR! Entity in Scene.AddEntity() is null");
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("ERROR! layer is null or empty in Scene.AddEntity()");
			_sceneLayers[layer].Add(e);
			e.Layer = layer;
		}

		public void RemoveEntity(Entity e, bool disposable=false, string layer="Objs")
		{
			if(e == null) throw new ArgumentNullException("ERROR! Entity in Scene.RemoveEntity() is null");
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("ERROR! layer is null or empty in Scene.RemoveEntity()");
			_sceneLayers[layer].Remove(e);
			e.Layer = "";
		}

		public bool ContainsEntity(Entity e, string layer="Objs")
		{
			if(e == null) throw new ArgumentNullException("ERROR! Entity in Scene.ContainsEntity() is null");
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("ERROR! layer is null or empty in Scene.ContainsEntity()");
			return _sceneLayers[layer].Contains(e);
		}

		public Entity GetEntity(Entity e, string layer="Objs")
		{
			if(e == null) throw new ArgumentNullException("ERROR! Entity in Scene.GetEntity() is null");
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("ERROR! layer is null or empty in Scene.GetEntity()");
			return _sceneLayers[layer].GetEntity(e);
		}

		public T GetEntityType<T>(string layer="Objs") where T : Entity
		{
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("ERROR! layer is null or empty in Scene.GetEntityType<>()");
			return (T)_sceneLayers[layer].GetEntityType<T>();
		}

		public void ClearLayer(string layer="Objs")
		{
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("ERROR! layer is null or empty in Scene.ClearLayer()");
			_sceneLayers[layer].Clear();
		}

		public void Clear()
		{
			_sceneLayers.Clear();
		}

		public int GetCountLayer(string layer)
		{
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("ERROR! layer is null or empty in Scene.GetCountLayer()");
			if(!_sceneLayers.ContainsKey(layer)) throw new KeyNotFoundException("ERROR! layer don't exist in Scene.GetCoutLayer");

			return _sceneLayers[layer].Count;
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
					foreach(var pair in _sceneLayers)
					{
						pair.Value.Dispose();
					}
					_sceneLayers.Clear();

					IsActive = false;
					Disposed = true;
				}
			}
		}

	}
}
