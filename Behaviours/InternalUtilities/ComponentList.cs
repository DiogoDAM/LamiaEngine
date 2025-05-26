using System;
using System.Collections;
using System.Collections.Generic;

namespace LamiaEngine
{
	public class ComponentList : IDisposable, IEnumerable<Component>, IEnumerable
	{
		public Entity Entity;

		public bool Disposed { get; private set; }
		public bool IsActive { get; private set; }


		private HashSet<Component> _toAdd;
		private HashSet<(bool, Component)> _toRemove;

		public List<Component> Components;
		public int Count { get { return Components.Count; } }

		public Component this[int index]
		{
			get
			{
				if(index < 0 || index >= Components.Count) throw new IndexOutOfRangeException("ERROR! Out of the index in ComponentList");
				return Components[index];
			}
		}

		public ComponentList(Entity entity)
		{
			Entity = entity;

			_toAdd = new();
			_toRemove = new();
			Components = new();
		}

		public void Start()
		{
			IsActive = true;
			_UpdateAdd();

			foreach(Component c in Components)
			{
				c.Start();
			}
		}

		public void Update()
		{
			if(Disposed || !IsActive) return;

			foreach(Component c in Components)
			{
				c.Update();
			}

			_UpdateAdd();
			_UpdateRemove();
		}

		public void Render()
		{
			if(Disposed || !IsActive) return;

			foreach(Component c in Components)
			{
				c.Render();
			}
		}

		public void Add(Component c)
		{
			if(c == null) throw new ArgumentNullException("ERROR! Component is null in ComponentList.Add()");
			_toAdd.Add(c);
		}

		public void Remove(Component c, bool disposable=false)
		{
			if(c == null) throw new ArgumentNullException("ERROR! Component is null in ComponentList.Remove()");
			_toRemove.Add((disposable, c));
		}

		public bool Contains(Component c) 
		{
			if(c == null) throw new ArgumentNullException("ERROR! Component is null in ComponentList.Contains()");
			return Components.Contains(c);
		}

		public Component GetComponent(Component c) 
		{
			if(c == null) throw new ArgumentNullException("ERROR! Component is null in ComponentList.GetComponent()");
			return Components.Find(c => c.Equals(c));
		}

		public T GetComponentType<T>() where T : Component
		{
			return (T)Components.Find(c => c is T);
		}

		public bool TryGetComponentType<T>(out T component) where T : Component
		{
			component = (T)Components.Find(c => c is T);
			return component != null;
		}

		public void Clear()
		{
			Components.Clear();
		}

        public IEnumerator<Component> GetEnumerator()
        {
            return Components.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
			return GetEnumerator();
        }

		private void _UpdateAdd()
		{
			foreach(Component c in _toAdd)
			{
				Components.Add(c);
				c.OnAdded();
			}

			_toAdd.Clear();
		}

		private void _UpdateRemove()
		{
			foreach(var v in _toRemove)
			{
				Component c = v.Item2;
				bool d = v.Item1;

				Components.Remove(c);
				c.OnRemoved();

				if(d) c.Dispose();
			}

			_toRemove.Clear();
		}

		public void Dispose()
		{
			if(!Disposed)
			{
				_toAdd.Clear();
				_toRemove.Clear();
				Components.Clear();
				IsActive = false;

				Disposed = true;
			}
		}

    }
}
