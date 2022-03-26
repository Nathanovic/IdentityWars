using UnityEngine;

namespace StateMachineStates {
	public class PlayerGatherState : StateWithData<DepletableResource> {

		[SerializeField] private PlayerDefaultState defaultState;
		[SerializeField] private GatherResource gatherBehaviour;

		private PlayerInput input;

		public override void Initialize(StateMachine stateMachine, IStateMachineTarget target) {
			base.Initialize(stateMachine, target);
			input = target.GetComponent<PlayerInput>();
		}

		protected override void OnEnter(DepletableResource resource) {
			if (resource == null) {
				Debug.LogWarning("Entered PlayerGatherState without resource. Aborting gather process.");
				stateMachine.EnterState(defaultState);
				return;
			}

			GatherResource.GatherResourceData behaviourData = new GatherResource.GatherResourceData(resource, OnFinishedGathering);
			gatherBehaviour.StartBehaviour(behaviourData);
		}

		protected override void OnExit() {
			gatherBehaviour.StopBehaviour();
		}

		protected override void OnUpdateRun() {
			Vector2 movementInput = input.PlayerActions.Movement.ReadValue<Vector2>();
			if (movementInput.magnitude > 0.1f) {
				stateMachine.EnterState(defaultState);
				return;
			}
		}

		private void OnFinishedGathering() {
			stateMachine.EnterState(defaultState);
		}

	}
}