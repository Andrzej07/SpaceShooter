using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceShooter
{
	public class ShipInput : MonoBehaviour
	{
		[SerializeField]
		private InputActionReference moveActionRef;
		[SerializeField]
		private InputActionReference shootActionRef;

		public ShipInputData GetCurrentInput()
		{
			var result = new ShipInputData
			{
				move = moveActionRef.action.ReadValue<Vector2>(),
				isFiring = shootActionRef.action.IsPressed()
			};

			return result;
		}
	}
}