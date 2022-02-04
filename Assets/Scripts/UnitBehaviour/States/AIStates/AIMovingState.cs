using UnityEngine;

namespace StateMachineStates {
	public class AIMovingState : StateWithData<AIMovingState.AIMovingStateData> {

		public class AIMovingStateData : StateData {
			public MoveAssignment MoveAssignment { get; private set; }

			public AIMovingStateData(MoveAssignment moveAssignment) {
				MoveAssignment = moveAssignment;
			}
		}

		[SerializeField] private MoveRigidbody rigidbodyMover;
		[SerializeField] private float bodyRadius;// Replace with Scriptable var

		private MoveAssignment moveAssignment;

		protected override void OnInitialize() {
			Rigidbody rigidbody = GetComponentInParent<Rigidbody>();
			rigidbodyMover.InitializeRigidbody(rigidbody);
		}

		protected override void OnEnter(AIMovingStateData stateData) {
			moveAssignment = stateData.MoveAssignment;
		}

		protected override void OnFixedUpdateRun() {
			Vector3 direction = moveAssignment.TargetPosition - rigidbodyMover.CurrentPosition;
			if (direction.magnitude <= bodyRadius) {
				stateMachine.EnterState<AIIdlingState>();
				return;
			}

			direction = Vector3.ClampMagnitude(direction, 1f);
			Vector3 movement3D = new Vector3(direction.x, 0f, direction.y);
			rigidbodyMover.AddForce(movement3D);
		}

	}
}