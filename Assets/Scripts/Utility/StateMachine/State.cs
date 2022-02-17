using UnityEngine;

namespace StateMachineStates {
	public abstract class State : MonoBehaviour {

		protected StateMachine stateMachine { get; private set; }

		public virtual void Initialize(StateMachine stateMachine, IStateMachineTarget target) {
			this.stateMachine = stateMachine;
			OnInitialize();
		}

		public virtual void Enter(IStateData stateData = null) {
			if (stateData != null) {
				Debug.LogWarning("Statedata is passed to state of type: " + GetType().ToString() + ". This statedata will not be " +
								"used because this class does not inherit from StateWithData");
			}
			OnEnter();
		}
		public void Exit() {
			OnExit();
		}
		public void Run() {
			OnUpdateRun();
		}
		public void FixedUpdateRun() {
			OnFixedUpdateRun();
		}

		protected virtual void OnInitialize() { }
		protected virtual void OnEnter() { }
		protected virtual void OnExit() { }
		protected virtual void OnUpdateRun() { }
		protected virtual void OnFixedUpdateRun() { }
	}
}