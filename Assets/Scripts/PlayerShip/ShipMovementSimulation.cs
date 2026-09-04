using Unity.Mathematics;
using UnityEngine;

namespace SpaceShooter
{
	public class ShipMovementSimulation : ShipSimulationComponent
	{
		[SerializeField]
		private Rigidbody2D rigidbody;
		[SerializeField]
		private float speed;
		[SerializeField]
		private Rect bounds;
		
		protected override void OnInitialize() { }
		
		public override void TickSimulation()
		{
			var positionDelta = Ship.LatestInput.move * speed * Time.fixedDeltaTime;

			if (positionDelta == Vector2.zero)
			{
				return;
			}
			
			Vector2 oldPosition = rigidbody.position;
			Vector2 newPosition = oldPosition + positionDelta;
			newPosition = Clamp(newPosition);

			if (newPosition != oldPosition)
			{
				rigidbody.MovePosition(newPosition);
			}
		}
		
		private Vector2 Clamp(Vector2 newPosition)
		{
			return math.clamp(newPosition, bounds.min, bounds.max);
		}
	}
}