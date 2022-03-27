using UnityEngine;

namespace StateMachineStates {
	public class PlayerGatherState : GatherState {

		private PlayerInput input;

		public override void Initialize(StateMachine stateMachine, IStateMachineTarget target) {
			base.Initialize(stateMachine, target);
			input = target.GetComponent<PlayerInput>();
		}

		protected override void OnEnter() {
			if (inventory.RemainingSpace <= 0) {
				EnterDefaultState();
				return;
			}

			GatherResource.GatherResourceData behaviourData = new GatherResource.GatherResourceData(targetResource, OnFinishedGathering);
			gatherBehaviour.StartBehaviour(behaviourData);
		}

		protected override void OnExit() {
			if (gatherBehaviour != null) {
				gatherBehaviour.StopBehaviour();
			}
		}

		protected override void OnUpdateRun() {
			Vector2 movementInput = input.PlayerActions.Movement.ReadValue<Vector2>();
			if (movementInput.magnitude > 0.1f) {
				EnterDefaultState();
				return;
			}
		}

		private void OnFinishedGathering() {
			EnterDefaultState();
		}

	}
}