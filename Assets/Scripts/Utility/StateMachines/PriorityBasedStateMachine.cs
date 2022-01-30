using System.Collections.Generic;
using UnityEngine;

public class PriorityBasedStateMachine : MonoBehaviour {

	private List<PriorityBasedState> states;
	private PriorityBasedState currentState;

	public bool CurrentStateIsOfType<T>() where T : PriorityBasedState {
		return currentState.GetType() == typeof(T);
	}

	public void SelectBestState(bool requireNewState = true) {
		foreach (PriorityBasedState state in states) {
			if (state == currentState) {
				if (requireNewState) { continue; }
				else { break; }
			}
			if (state.CanStart()) {
				EnterState(state);
				break;
			}
		}
	}

	private void Awake() {
		states = new List<PriorityBasedState>(GetComponentsInChildren<PriorityBasedState>());
		IStateMachineTarget target = GetComponent<IStateMachineTarget>();
		foreach (PriorityBasedState playerState in states) {
			playerState.Initialize(this, target);
		}
	}

	private void Update() {
		SelectBestState(false);
		currentState.Run();
	}

	private void FixedUpdate() {
		currentState.FixedUpdateRun();
	}

	private void EnterState(PriorityBasedState state) {
		if (state == currentState) { return; }
		if (currentState != null) {
			currentState.Deactivate();
		}
		currentState = state;
		currentState.Activate();
	}

}