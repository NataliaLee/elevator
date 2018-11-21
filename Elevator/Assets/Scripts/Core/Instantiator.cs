using Assets.Scripts.Logs;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core
{
	public class Instantiator
	{
		private readonly ICustomLogger _customLogger;
		private readonly DiContainer _diContainer;

		public Instantiator(ICustomLogger customLogger, DiContainer diContainer)
		{
			_customLogger = customLogger;
			_diContainer = diContainer;
		}

		public GameObject LoadGameObjectFromReference(GameObject gameObject, Transform parentTransform = null)
		{
			if (gameObject == null)
			{
				_customLogger.LogError($"gameObject is null! Can't Instantiate it!");
				return null;
			}

			var instantiatedObject = ReferenceEquals(parentTransform, null)
				? _diContainer.InstantiatePrefab(gameObject)
				: _diContainer.InstantiatePrefab(gameObject, parentTransform);
			return instantiatedObject;
		}

		public T LoadGameObjectOnScene<T>(string path, Transform parentTransform = null) where T : class
		{
			bool loadError;
			T loadedObject = null;
			var loadedPrefab = Resources.Load<GameObject>(path);
			if (!ReferenceEquals(loadedPrefab, null))
			{
				var instantiatedObject = ReferenceEquals(parentTransform, null)
						? _diContainer.InstantiatePrefab(loadedPrefab)
						: _diContainer.InstantiatePrefab(loadedPrefab, parentTransform);
					//? Object.Instantiate(loadedPrefab)
					//: Object.Instantiate(loadedPrefab, parentTransform);
				if (typeof(T) != typeof(GameObject))
				{
					loadedObject = instantiatedObject.GetComponent<T>();
					if (ReferenceEquals(loadedObject, null))
					{
						_customLogger.LogError($"Error in getting {typeof(T)} from game object!");
						loadError = true;
					}
					else
					{
						loadError = false;
					}
				}
				else
				{
					loadedObject = instantiatedObject as T;
					loadError = false;
				}
			}
			else
			{
				_customLogger.LogError($"Error in loading game object! Path: {path} failed. ");
				loadError = true;
			}

			if (loadError)
			{
				_customLogger.LogError($"Can\'t load requested object {typeof(T)} by path {path}");
				return null;
			}

			return loadedObject;
		}
	}
}