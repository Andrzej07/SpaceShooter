using UnityEngine;

namespace SpaceShooter
{
	public class ShipTilt : ShipVisualComponent
	{
		[SerializeField]
		private Transform tiltedTransform;
		[SerializeField]
		private Vector3 tiltAxis;
		[SerializeField]
		private float tiltAngleMax;
		[SerializeField]
		private float tiltSpeed;

		private Vector3 baseAngles;

		protected override void OnInitialize()
		{
			baseAngles = tiltedTransform.localRotation.eulerAngles;
		}
		
		public override void TickVisuals()
		{
			SetTiltBasedOnInput(Ship.LatestInput.move.y);
		}

		private void SetTiltBasedOnInput(float inputValue)
		{
			Vector3 currentAngles = tiltedTransform.localRotation.eulerAngles;
			float tiltAngle = GetTargetTiltAngleFromInput(inputValue);
			var targetAngles = baseAngles + tiltAxis * tiltAngle;
			float angleDelta = tiltSpeed * Time.deltaTime;
			Vector3 newAngles = currentAngles.MoveTowardsPerComponent(targetAngles, angleDelta);
			
			tiltedTransform.localRotation = Quaternion.Euler(newAngles);
		}

		private float GetTargetTiltAngleFromInput(float inputValue)
		{
			float tiltAngle = 0;

			if (inputValue < 0)
			{
				tiltAngle = -tiltAngleMax;
			}
			else if(inputValue > 0)
			{
				tiltAngle = tiltAngleMax;
			}

			return tiltAngle;
		}
	}
}