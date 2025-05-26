using System;
using Microsoft.Xna.Framework;

namespace LamiaEngine
{
	public abstract class Entity : IDisposable
	{
		public ComponentList Components;

		public bool IsActive { get; protected set; }
		public bool Disposed { get; protected set; }

		public Transform Transform;

		public string Layer;
		public string Tag { get; protected set; }
		public string Name { get; protected set; }

		public int Width, Height;

		public Scene Scene;

		public Entity(Scene scene)
		{
			Scene = scene;

			Components = new(this);
			Transform = new();

			Width = Engine.TileSize;
			Height = Engine.TileSize;
		}

		public Entity()
		{
			Components = new(this);
			Transform = new();
		}

		public virtual void Start()
		{
			IsActive = true;

			Components.Start();
		}

		public virtual void Update()
		{

			Components.Update();
		}

		public virtual void Render()
		{

			Components.Render();
		}

		public void OnAdded(Scene scene)
		{
			Scene = scene;
		}

		public void OnRemoved()
		{
			Scene = null;
		}

		//Components Methods
		public void AddComponent(Component c) 
		{
			if(c == null) throw new ArgumentNullException("ERROR! Component is null in Entity.AddComponent()");

			Components.Add(c);
			c.Entity = this;
		}

		public T RegisterComponent<T>() where T : Component, new()
		{
			T component = new T();

			Components.Add(component);

			component.Entity = this;

			return component;
		}

		public void RemoveComponent(Component c) 
		{
			if(c == null) throw new ArgumentNullException("ERROR! Component is null in Entity.RemoveComponent()");

			Components.Remove(c);
		}

		public bool ContainsComponent(Component c) 
		{
			if(c == null) throw new ArgumentNullException("ERROR! Component is null in Entity.ContainsComponent()");

			return Components.Contains(c);
		}

		public Component GetComponent(Component c)
		{
			if(c == null) throw new ArgumentNullException("ERROR! Component is null in Entity.GetComponent()");

			return Components.GetComponent(c);
		}

		public T GetComponentType<T>() where T : Component
		{
			return (T)Components.GetComponentType<T>();
		}

		public bool TryGetComponentType<T>(out T component) where T : Component
		{
			component = (T)Components.GetComponentType<T>();
			return component != null;
		}


		// Scenes Methods
		public static void AddToScene(Entity e, string layer="Objs")
		{
			if(e == null) throw new ArgumentNullException("ERROR! Entity is null in AddToScene()");
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("ERROR! layer is null or empty in AddToScene()");

			SceneManager.Instance.CurrentScene.AddEntity(e, layer);
			e.Start();
		}

		public static void RemoveFromScene(Entity e, string layer="Objs")
		{
			if(e == null) throw new ArgumentNullException("ERROR! Entity is null in RemoveFromScene()");
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("ERROR! layer is null or empty in RemoveFromScene()");

			SceneManager.Instance.CurrentScene.RemoveEntity(e, false, layer);
		}

		public static T Instantiate<T>(string layer="Objs") where T : Entity, new()
		{
			T entity = new T();
			SceneManager.Instance.CurrentScene.AddEntity(entity, layer);
			entity.Start();
			return entity;
		}

		public static T Instantiate<T>(Transform transform, string layer="Objs") where T : Entity, new()
		{
			T entity = new T();
			entity.Transform = transform;
			SceneManager.Instance.CurrentScene.AddEntity(entity, layer);
			entity.Start();
			return entity;
		}

		public static void Destroy(Entity e, string layer="Objs")
		{
			if(e == null) throw new ArgumentNullException("ERROR! Entity is null in Destroy()");
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("ERROR! layer is null or empty in Destroy()");

			SceneManager.Instance.CurrentScene.RemoveEntity(e, true, layer);
		}

		//Utilities Methods 
		public void CenterEntityOnScreen()
		{
			Transform.Position = new Vector2(Engine.WindowWidth*.5f, Engine.WindowHeight*.5f);
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
					Components.Dispose();
					Transform = null;
					Scene = null;

					IsActive = false;
					Disposed = true;
				}
			}
		}

	}
}
