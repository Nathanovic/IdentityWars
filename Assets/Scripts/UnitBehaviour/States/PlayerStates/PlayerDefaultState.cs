using UnityEngine;

namespace StateMachineStates {
	public class PlayerDefaultState : PlayerState {

		[SerializeField] private MoveRigidbody rigidbodyMover;

		protected override void OnInitialize() {
			Rigidbody rigidbody = target.GetComponent<Rigidbody>();
			rigidbodyMover.InitializeRigidbody(rigidbody);
		}

		protected override void OnEnter() {
			//input.PlayerActions.Movement.canceled += OnCancelMovementInput;
		}

		protected override void OnFixedUpdateRun() {
			Vector2 movementInput = input.PlayerActions.Movement.ReadValue<Vector2>();
			movementInput = Vector2.ClampMagnitude(movementInput, 1f);
			Vector3 movement3D = new Vector3(movementInput.x, 0f, movementInput.y);
			rigidbodyMover.AddForce(movement3D);
		}

	}
}