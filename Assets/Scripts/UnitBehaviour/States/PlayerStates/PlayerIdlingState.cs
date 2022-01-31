using UnityEngine.InputSystem;

namespace StateMachineStates {
	public class PlayerIdlingState : PlayerState {

		protected override void OnEnter() {
			input.PlayerActions.Movement.performed += OnMovementInput;
		}

		private void OnMovementInput(InputAction.CallbackContext context) {
			input.PlayerActions.Movement.performed -= OnMovementInput;
			stateMachine.EnterState<PlayerMovingState>();
		}

	}
}