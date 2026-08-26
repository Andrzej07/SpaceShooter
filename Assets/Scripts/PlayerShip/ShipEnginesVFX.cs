using UnityEngine;

namespace SpaceShooter
{
	public class ShipEnginesVFX : ShipVisualComponent
	{
		[SerializeField]
		private ParticleSystem mainEffect;
		[SerializeField]
		private Vector2 mainEffectLifetimeMinMax;
		[SerializeField]
		private ParticleSystem leftEffect;
		[SerializeField]
		private ParticleSystem rightEffect;
		
		protected override void OnInitialize()
		{
			SetSystemEnabled(leftEffect, false);
			SetSystemEnabled(rightEffect, false);
		}

		public override void TickVisuals()
		{
			var moveInput = Ship.LatestInput.move;
			
			SetSystemEnabled(leftEffect, moveInput.y < 0);
			SetSystemEnabled(rightEffect, moveInput.y > 0);
			
			float mainEffectLifetime = GetMainEffectLifetime(moveInput);
			var mainEffectMain = mainEffect.main;
			mainEffectMain.startLifetimeMultiplier = mainEffectLifetime;
		}

		private float GetMainEffectLifetime(Vector2 moveInput)
		{
			float mainEffectEmissionRate = mainEffectLifetimeMinMax.ComponentsAverage();

			if (moveInput.x < 0)
			{
				mainEffectEmissionRate = mainEffectLifetimeMinMax.x;
			}
			else if (moveInput.x > 0)
			{
				mainEffectEmissionRate = mainEffectLifetimeMinMax.y;
			}

			return mainEffectEmissionRate;
		}

		private void SetSystemEnabled(ParticleSystem system, bool value)
		{
			var emission = system.emission;
			emission.enabled = value;
		}
	}
}