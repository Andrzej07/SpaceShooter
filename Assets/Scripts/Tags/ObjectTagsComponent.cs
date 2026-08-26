using UnityEngine;

namespace SpaceShooter
{
	public class ObjectTagsComponent : MonoBehaviour
	{
		[SerializeField]
		private ObjectTags tags;

		public ObjectTags Tags => tags;
	}
}