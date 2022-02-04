using UnityEngine;

public class AssignmentReceiver : MonoBehaviour, ISelectable, IStateMachineTarget {

	public delegate void OnAssignmentReceivedDelegate(IAssignmentTarget assignmentTarget);
	public event OnAssignmentReceivedDelegate OnAssignmentReceived;

	public IAssignmentTarget CurrentAssignment { get; private set; }

	private void Start() {
		GetComponentInChildren<StateMachine>().Initialize(this);// Should not be here
	}

	public void Assign(Faction faction, IAssignmentTarget assignmentTarget) {
		OnAssignmentReceived?.Invoke(assignmentTarget);
	}

	public IAssignmentTarget Interact() {
		return new AttackAssignment();
	}

	public void Select(Faction faction) {
		Debug.Log("Selected " + transform.name);
	}

}