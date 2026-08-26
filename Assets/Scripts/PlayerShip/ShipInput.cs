using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceShooter
{
	public class ShipInput : MonoBehaviour
	{
		[SerializeField]
		private string moveActionName;
		[SerializeField]
		private string shootActionName;
		
		private InputAction moveAction;
		private InputAction shootAction;

		public void Initialize()
		{
			moveAction = InputSystem.actions.FindAction(moveActionName);
			shootAction = InputSystem.actions.FindAction(shootActionName);
		}

		public ShipInputData GetCurrentInput()
		{
			var result = new ShipInputData
			{
				move = moveAction.ReadValue<Vector2>(),
				isFiring = shootAction.IsPressed()
			};

			return result;
		}
	}
}