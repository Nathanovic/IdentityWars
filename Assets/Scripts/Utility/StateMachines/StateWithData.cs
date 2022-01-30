using UnityEngine;

namespace StateMachineStates {
	public abstract class StateWithData<T> : State where T : State.StateData {
		public new void Enter() {
			Debug.LogWarning("Trying to access a StateWithData without giving the data! Aborting...");
		}
		public void Enter(T stateData) {
			OnEnter(stateData);
		}

		protected override void OnEnter() {
			Debug.LogWarning("Trying to call OnEnter on a state with data without giving a type parameter!");
		}
		protected abstract void OnEnter(T stateData);
	}
}