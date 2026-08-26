using UnityEngine;

namespace SpaceShooter
{
	public class ShipMovementSimulation : ShipSimulationComponent
	{
		[SerializeField]
		private new Rigidbody2D rigidbody;
		[SerializeField]
		private float speed;
		
		protected override void OnInitialize() { }
		
		public override void TickSimulation()
		{
			var positionDelta = Ship.LatestInput.move * speed * Time.fixedDeltaTime;
			rigidbody.MovePosition(rigidbody.position + positionDelta);
		}
	}
}