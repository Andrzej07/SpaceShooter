using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	public class PlayerShip : MonoBehaviour
	{
		[SerializeField]
		private ShipInput shipInput;
		[SerializeField]
		private ShipVisualComponent[] visualComponents;
		[SerializeField]
		private ShipSimulationComponent[] simulationComponents;

		public ShipInputData LatestInput { get; private set; }
		
		private void Start()
		{
			shipInput.Initialize();
			
			InitializeComponents(visualComponents);
			InitializeComponents(simulationComponents);

			void InitializeComponents(IReadOnlyList<ShipComponent> shipComponents)
			{
				foreach (ShipComponent shipVisualComponent in shipComponents)
				{
					shipVisualComponent.Initialize(this);
				}
			}
		}

		private void Update()
		{
			LatestInput = shipInput.GetCurrentInput();
			
			foreach (ShipVisualComponent shipVisualComponent in visualComponents)
			{
				shipVisualComponent.TickVisuals();
			}
		}

		private void FixedUpdate()
		{
			foreach (ShipSimulationComponent shipSimulationComponent in simulationComponents)
			{
				shipSimulationComponent.TickSimulation();
			}
		}
	}
}