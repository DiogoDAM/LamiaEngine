using System;
using System.Collections;
using System.Collections.Generic;

namespace LamiaEngine
{
	public class EntityList : IDisposable, IEnumerable<Entity>, IEnumerable
	{
		public List<Entity> Entities { get; private set; }

		public bool IsActive { get; private set; }
		public bool Disposed { get; private set; }

		public int Count { get { return Entities.Count; } }

		private HashSet<Entity> _toAdd;
		private HashSet<(bool, Entity)> _toRemove;

		public Scene Scene;

		public Entity this[int index]
		{
			get 
			{
				if(index < 0 || index >= Entities.Count) throw new IndexOutOfRangeException("ERROR! out of index in EntityList");
				return Entities[index];
			}
		}

		public EntityList(Scene scene)
		{
			Scene = scene;

			Entities = new();

			_toAdd = new();
			_toRemove = new();
		}

		public void Start()
		{
			IsActive = true;

			_UpdateAdd();

			foreach(Entity e in Entities)
			{
				e.Start();
			}
		}

		public void Update()
		{
			if(!IsActive || Disposed) return;

			foreach(Entity e in Entities)
			{
				e.Update();
			}

			_UpdateAdd();
			_UpdateRemove();
		}

		public void Render()
		{
			if(!IsActive || Disposed) return;

			foreach(Entity e in Entities)
			{
				e.Render();
			}
		}

		public void Add(Entity e)
		{
			if(e == null) throw new ArgumentNullException("ERROR! Entity in EntityList.Add() is null");
			_toAdd.Add(e);
		}

		public void Remove(Entity e, bool disposable=false)
		{
			if(e == null) throw new ArgumentNullException("ERROR! Entity in EntityList.Remove() is null");
			_toRemove.Add((disposable, e));
		}

		public bool Contains(Entity e)
		{
			if(e == null) throw new ArgumentNullException("ERROR! Entity in EntityList.Contais() is null");
			return Entities.Contains(e);
		}

		public void Clear()
		{
			Entities.Clear();
		}

		public Entity GetEntity(Entity e)
		{
			if(e == null) throw new ArgumentNullException("ERROR! Entity in EntityList.GetEntity() is null");
			return Entities.Find(v => v.Equals(e));
		}

		public T GetEntityType<T>() where T : Entity
		{
			return (T)Entities.Find(e => e is T);
		}

		public IEnumerator<Entity> GetEnumerator()
		{
			return Entities.GetEnumerator();
		}

        IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private void _UpdateAdd()
		{
			foreach(Entity e in _toAdd)
			{
				Entities.Add(e);
				e.OnAdded(Scene);
			}

			_toAdd.Clear();

		}

		private void _UpdateRemove()
		{
			foreach(var v in _toRemove)
			{
				bool d = v.Item1;
				Entity e = v.Item2;

				Entities.Remove(e);
				e.OnRemoved();

				if(d) e.Dispose();
			}

			_toRemove.Clear();

		}

		public void Dispose()
		{
			if(!Disposed)
			{
				Entities.Clear();
				_toAdd.Clear();
				_toRemove.Clear();

				IsActive = false;
				Disposed = true;
			}
		}

	}
}
