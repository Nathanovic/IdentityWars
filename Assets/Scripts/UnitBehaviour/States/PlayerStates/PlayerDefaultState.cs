using UnityEngine;

namespace StateMachineStates {
	public class PlayerDefaultState : State {

		[SerializeField] private MoveRigidbody rigidbodyMover;

		private PlayerInput input;

		public override void Initialize(StateMachine stateMachine, IStateMachineTarget target) {
			base.Initialize(stateMachine, target);
			input = target.GetComponent<PlayerInput>();
			Rigidbody rigidbody = target.GetComponent<Rigidbody>();
			rigidbodyMover.InitializeRigidbody(rigidbody);
		}

		protected override void OnFixedUpdateRun() {
			Vector2 movementInput = input.PlayerActions.Movement.ReadValue<Vector2>();
			movementInput = Vector2.ClampMagnitude(movementInput, 1f);
			Vector3 movement3D = new Vector3(movementInput.x, 0f, movementInput.y);
			rigidbodyMover.AddForce(movement3D);
		}

	}
}