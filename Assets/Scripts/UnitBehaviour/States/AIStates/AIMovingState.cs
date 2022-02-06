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

		private AssignmentReceiver assignmentReceiver;
		private MoveAssignment assignment;

		public override void Initialize(StateMachine stateMachine, IStateMachineTarget target) {
			base.Initialize(stateMachine, target);
			MonoBehaviour targetMonoBehaviour = (MonoBehaviour)target;
			assignmentReceiver = targetMonoBehaviour.GetComponent<AssignmentReceiver>();
			Rigidbody rigidbody = targetMonoBehaviour.GetComponent<Rigidbody>();
			rigidbodyMover.InitializeRigidbody(rigidbody);
		}

		private void OnAssignmentReceived(IAssignmentTarget assignmentTarget) {
			MoveAssignment moveAssignment = (MoveAssignment)assignmentTarget;
			if (moveAssignment != null) {
				assignment = moveAssignment;
			}
		}

		protected override void OnEnter(AIMovingStateData stateData) {
			assignmentReceiver.OnAssignmentReceived += OnAssignmentReceived;
			assignment = stateData.MoveAssignment;
		}

		protected override void OnFixedUpdateRun() {
			Vector3 direction = assignment.TargetPosition - rigidbodyMover.CurrentPosition;
			direction = new Vector3(direction.x, 0f, direction.z);
			if (direction.magnitude <= bodyRadius) {
				FinishMovingBehaviour();
				return;
			}

			rigidbodyMover.AddForce(direction.normalized);
		}

		protected override void OnExit() {
			assignmentReceiver.OnAssignmentReceived -= OnAssignmentReceived;
		}

		private void FinishMovingBehaviour() {
			GatherResourceAssignment gatherResourceAssignment = (GatherResourceAssignment)assignment;
			if (gatherResourceAssignment != null) {
				Debug.Log("Gather Resource!");
			}
			stateMachine.EnterState<AIIdlingState>();
		}

	}
}