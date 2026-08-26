using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceShooter
{
	public class ShipInput : MonoBehaviour
	{
		[SerializeField]
		private string moveActionName;
		
		private InputAction moveAction;

		public void Initialize()
		{
			moveAction = InputSystem.actions.FindAction(moveActionName);
		}

		public ShipInputData GetCurrentInput()
		{
			var result = new ShipInputData
			{
				move = moveAction.ReadValue<Vector2>()
			};

			return result;
		}
	}
}