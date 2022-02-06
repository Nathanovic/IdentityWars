using StateMachineStates;
using System.Collections.Generic;
using UnityEngine;

// Selectable & Interactable lostrekken
// AssignmentReceiver (Selectable)
// Attackable (Interactable)
public class AssignmentReceiver : MonoBehaviour, ISelectable, IStateMachineTarget {

	public delegate void OnAssignmentReceivedDelegate(IAssignmentTarget assignmentTarget);
	public event OnAssignmentReceivedDelegate OnAssignmentReceived;

	// serializable in editor <ObjectType, Gedrag>
	// [0] Boom, BomenknuffelenBehaviour
	// [1] Rock, GatherResource

	//ScriptableObj
	public enum ObjectType {
		Tree,Rock,Minion,Player
	}
	private class ObjectAssignmentPair {
		public ObjectType ObjectType;
		public State State;
	}

	[SerializeField] private List<ObjectAssignmentPair> assignmentDictionary;

	[SerializeField] private StateMachine stateMachine;

	public IAssignmentTarget CurrentAssignment { get; private set; }

	private void Start() {
		GetComponentInChildren<StateMachine>().Initialize(this);// Should not be here (temporary solution)
	}

	public void Assign(Faction faction, IAssignmentTarget assignmentTarget) {
		OnAssignmentReceived?.Invoke(assignmentTarget);
		State state = assignmentDictionary.Find((x) => x.ObjectType == assignmentTarget.ObjectType);

		if (state != null) {
			
			stateMachine.EnterState(state);// use params for StateData
		}
	}

	public IAssignmentTarget Interact() {
		return new AttackAssignment();// Too much hard coded
	}

	public void Select(Faction faction) {
		Debug.Log("Selected " + transform.name);
	}

}