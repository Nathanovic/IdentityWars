using UnityEngine;

namespace StateMachineStates {
	public abstract class State : MonoBehaviour {

		public abstract class StateData { }

		protected StateMachine stateMachine { get; private set; }
		protected bool isActive { get; private set; }

		public virtual void Initialize(StateMachine stateMachine, IStateMachineTarget target) {
			this.stateMachine = stateMachine;
			OnInitialize();
		}

		public void Enter() {
			isActive = true;
			OnEnter();
		}
		public void Exit() {
			isActive = false;
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