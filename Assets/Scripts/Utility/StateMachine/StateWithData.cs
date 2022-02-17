using UnityEngine;

namespace StateMachineStates {
	public abstract class StateWithData<T> : State where T : IStateData {

		// Seal the protected OnEnter without parameters to prevent confusion
		protected sealed override void OnEnter() {
			Debug.Log("This override method should never be called :P");
		}

		public sealed override void Enter(IStateData stateData = null) {
			if (stateData == null) {
				Debug.LogError("Trying to enter StateWithData without passing in a IStateData parameter. This is not allowed!");
				return;
			}

			T castedStateData = (T)stateData;
			if (castedStateData == null) {
				Debug.LogWarning("State data: " + stateData.ToString() + " cannot be casted to type: " + typeof(T).ToString());
				return;
			}

			OnEnter(castedStateData);
		}

		protected abstract void OnEnter(T stateData);

	}

}