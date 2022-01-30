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

	public void QueueState<T>(bool continueQueue = false) where T : State {
		stateQueue.Add(() => {
			EnterState<T>(false);
		});

		if (continueQueue) {
			ContinueQueue();
		}
	}

	public void QueueState<T, S>(S stateData, bool continueQueue = false) where T : StateWithData<S> where S : State.StateData {
		stateQueue.Add(() => {
			EnterState<T, S>(stateData, false);
		});

		if (continueQueue) {
			ContinueQueue();
		}
	}

	public void ContinueQueue() {
		if (stateQueue.Count == 0) { return; }
		stateQueue[0].Invoke();
		stateQueue.RemoveAt(0);
	}

	public void ClearQueue() {
		stateQueue.Clear();
	}

	public void EnterState<T>(bool clearQueue = true) where T : State {
		if (clearQueue) {
			ClearQueue();
		}
		State state = GetState<T>();
		EnterState(state);
	}
	public void EnterState<T, S>(S stateData, bool clearQueue = true) where T : StateWithData<S> where S : State.StateData {
		if (clearQueue) {
			ClearQueue();
		}
		StateWithData<S> state = GetState<T>();
		if (state != null) {
			EnterStateWithData(state, stateData);
		}
	}

	public Type GetCurrentStateType() {
		return currentState.GetType();
	}

	protected T GetState<T>() where T : State {
		foreach (State state in states) {
			if (state.GetType() == typeof(T)) {
				return (T)state;
			}
		}

		Debug.LogWarning("Couldn't find state of type " + typeof(T).ToString(), this);
		return null;
	}

	protected virtual void Update() {
		if (currentState == null) { return; }
		currentState.Run();
	}

	private void FixedUpdate() {
		if (currentState == null) { return; }
		currentState.FixedUpdateRun();
	}

	protected void EnterState(State state) {
		if (state == currentState) { return; }

		if (state == null) {
			Debug.LogWarning("Unknown state of type: " + state.GetType().Name + ", can't enter this state!");
			return;
		}
		if (debugLogStateTransitions) {
			Debug.Log("Entering next state: " + state.GetType().Name);
		}

		if (currentState != null) {
			currentState.Exit();
		}
		currentState = state;
		currentState.Enter();
	}

	protected void EnterStateWithData<S>(StateWithData<S> state, S stateData) where S : State.StateData {
		if (debugLogStateTransitions) {
			Debug.Log("Entering next state: " + state.GetType().Name);
		}

		if (state == null) {
			Debug.LogWarning("Unknown state of type: " + state.GetType().Name + ", can't enter this state!");
			return;
		}

		if (currentState != null) {
			currentState.Exit();
		}

		currentState = state;
		state.Enter(stateData);
	}

}