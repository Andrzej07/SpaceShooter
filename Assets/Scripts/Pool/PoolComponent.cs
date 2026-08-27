using UnityEngine;
using UnityEngine.Pool;

namespace SpaceShooter
{
	public abstract class PoolComponent<T> : MonoBehaviour where T : MonoBehaviour, IPooledObject<T>
	{
		[SerializeField]
		private T elementPrefab;
	
		private ObjectPool<T> objectPool;

		protected ObjectPool<T> ObjectPool => objectPool;

		private void Awake()
		{
			objectPool = new ObjectPool<T>(CreateFunc, actionOnRelease: ActionOnRelease);
			T CreateFunc()
			{
				var element = Instantiate(elementPrefab);
				element.ReturnToPoolAction = Release;

				return element;
			}

			void ActionOnRelease(T obj)
			{
				obj.gameObject.SetActive(false);
			}
		}

		public void Release(T element)
		{
			objectPool.Release(element);
		}
	}
}