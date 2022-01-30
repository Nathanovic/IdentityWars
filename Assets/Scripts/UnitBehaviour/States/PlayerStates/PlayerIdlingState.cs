using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachineStates {
	public class PlayerIdlingState : PlayerState {

		protected override void OnEnter() {
			target.Input.Movement.performed += OnMovementInput;
		}

		private void OnMovementInput(InputAction.CallbackContext context) {
			target.Input.Movement.performed -= OnMovementInput;
			stateMachine.EnterState<PlayerMovingState>();
		}

	}
}