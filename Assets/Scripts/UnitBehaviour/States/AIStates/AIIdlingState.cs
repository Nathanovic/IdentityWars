using UnityEngine;

namespace StateMachineStates {
	public class AIIdlingState : State {

		private AssignmentReceiver assignmentReceiver;

		public override void Initialize(StateMachine stateMachine, IStateMachineTarget target) {
			base.Initialize(stateMachine, target);
			assignmentReceiver = ((MonoBehaviour)target).GetComponent<AssignmentReceiver>();
		}

		protected override void OnEnter() {
			assignmentReceiver.OnAssignmentReceived += OnAssignmentReceived;
		}

		protected override void OnExit() {
			assignmentReceiver.OnAssignmentReceived -= OnAssignmentReceived;
		}

		private void OnAssignmentReceived(IAssignmentTarget assignmentTarget) {


			MoveAssignment moveAssignment = (MoveAssignment)assignmentTarget;
			if (moveAssignment != null) {
				AIMovingState.AIMovingStateData movingStateData = new AIMovingState.AIMovingStateData(moveAssignment);
				stateMachine.EnterState<AIMovingState, AIMovingState.AIMovingStateData>(movingStateData);
			}
		}
	}
}