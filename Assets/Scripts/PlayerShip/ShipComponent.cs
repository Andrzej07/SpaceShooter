using UnityEngine;

namespace SpaceShooter
{
	public abstract class ShipComponent : MonoBehaviour
	{
		protected PlayerShip Ship { get; private set; }
		
		public void Initialize(PlayerShip ship)
		{
			Ship = ship;
			OnInitialize();
		}
		
		protected abstract void OnInitialize();
	}
}