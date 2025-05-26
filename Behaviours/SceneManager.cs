using System;
using System.Collections.Generic;

namespace LamiaEngine
{
	public class SceneManager
	{
		private static SceneManager _instance;
		public static SceneManager Instance
		{
			get
			{
				if(_instance == null) _instance = new();
				return _instance;
			}
		}

		private Dictionary<string, Scene> _scenes;
		public int Count { get { return _scenes.Count; } }

		public Scene CurrentScene;

		private SceneManager()
		{
			_scenes = new();
		}

		public void Start()
		{
			CurrentScene?.Start();
		}

		public void Update()
		{
			CurrentScene?.Update();
		}

		public void Render()
		{
			CurrentScene?.Render();
		}

		public void RenderUi()
		{
			CurrentScene?.RenderUi();
		}

		public void AddScene(string sceneName, Scene scene)
		{
			if(string.IsNullOrEmpty(sceneName)) throw new ArgumentNullException("ERROR! sceneName is null or empty in SceneManager.AddScene()");
			if(scene == null) throw new ArgumentNullException("ERROR! scene is null in SceneManager.AddScene()");

			if(Count == 0) CurrentScene = scene;

			_scenes.Add(sceneName, scene);
			scene.OnAdded();
		}

		public void RemoveScene(string sceneName)
		{
			if(string.IsNullOrEmpty(sceneName)) throw new ArgumentNullException("ERROR! sceneName is null or empty in SceneManager.RemoveScene()");

			if(!_scenes.ContainsKey(sceneName)) throw new Exception("ERROR! sceneName don't exist in SceneManager");

			Scene s = _scenes[sceneName];

			_scenes.Remove(sceneName);
			s.OnRemoved();
		}

		public bool ContainsScene(string sceneName)
		{
			if(string.IsNullOrEmpty(sceneName)) throw new ArgumentNullException("ERROR! sceneName is null or empty in SceneManager.ContainsScene()");

			return _scenes.ContainsKey(sceneName);
		}

		public void ChangeScene(string sceneName)
		{
			if(string.IsNullOrEmpty(sceneName)) throw new ArgumentNullException("ERROR! sceneName is null or empty in SceneManager.ChangeScene()");

			if(!_scenes.ContainsKey(sceneName)) throw new Exception("ERROR! sceneName don't exist in SceneManager");

			CurrentScene.Desactive();
			CurrentScene = _scenes[sceneName];
			CurrentScene.Active();
		}
	}
}
