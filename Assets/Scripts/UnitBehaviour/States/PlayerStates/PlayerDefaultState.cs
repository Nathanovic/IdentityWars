using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachineStates {
	public class PlayerDefaultState : State {

		[SerializeField] private PlayerGatherState gatherstate;
		[SerializeField] private MoveWithInput inputBasedMovement;
		[SerializeField] private float moveSpeed;

		private PlayerInput input;
		private TriggerListener triggerListener;
		private MoveWithInput.MoveWithInputData moveBehaviourData;

		public override void Initialize(StateMachine stateMachine, IStateMachineTarget target) {
			base.Initialize(stateMachine, target);
			input = target.GetComponent<PlayerInput>();
			triggerListener = target.GetComponent<TriggerListener>();
			moveBehaviourData = new MoveWithInput.MoveWithInputData(GetMovementInput, moveSpeed);
		}

		protected override void OnEnter() {
			input.PlayerActions.Fire.performed += OnFireButton;
			inputBasedMovement.Start(moveBehaviourData);
		}

		private void OnFireButton(InputAction.CallbackContext context) {
			// If there is a resource nearby, we should start gathering this resource
			DepletableResource resource = triggerListener.GetNearbyComponentOfType<DepletableResource>(true);
			if (resource != null) {
				stateMachine.EnterState(gatherstate, resource);
			}
		}

		protected override void OnExit() {
			input.PlayerActions.Fire.performed -= OnFireButton;
		}

		private Vector2 GetMovementInput() {
			return input.PlayerActions.Movement.ReadValue<Vector2>();
		}

	}
}