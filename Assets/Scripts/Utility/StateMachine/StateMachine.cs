using System;
using System.Collections.Generic;
using UnityEngine;
using StateMachineStates;

public class StateMachine : MonoBehaviour {

	[SerializeField] private State startState;
	[Space]
	[SerializeField] private bool debugLogStateTransitions;

	private State[] states;
	private State currentState;

	private List<Action> stateQueue;

	public void Initialize(IStateMachineTarget target) {
		if (states == null) {
			states = GetComponentsInChildren<State>();
			foreach (State state in states) {
				state.Initialize(this, target);
			}

			stateQueue = new List<Action>();
		}
		else {
			ClearQueue();
		}

		if (startState != null) {
			EnterState(startState);
		}
	}

	public void QueueState(State state, IStateData stateData = null) {
		stateQueue.Add(() => {
			EnterState(state, stateData);
		});
	}

	public void ContinueQueue() {
		if (stateQueue.Count == 0) { return; }
		stateQueue[0].Invoke();
		stateQueue.RemoveAt(0);
	}

	public void ClearQueue() {
		stateQueue.Clear();
	}

	public void EnterState(State state, IStateData stateData = null, bool clearQueue = true) {
		if (clearQueue) {
			ClearQueue();
		}

		EnterState(state, stateData);
	}

	public Type GetCurrentStateType() {
		return currentState.GetType();
	}

	protected void Update() {
		if (currentState == null) { return; }
		currentState.Run();
	}

	private void FixedUpdate() {
		if (currentState == null) { return; }
		currentState.FixedUpdateRun();
	}

	protected void EnterState(State state, IStateData stateData) {
		if (debugLogStateTransitions) {
			Debug.Log("Entering next state: " + state.GetType().Name);
		}

		if (currentState != null) {
			currentState.Exit();
		}
		currentState = state;
		currentState.Enter(stateData);
	}

}