using StateMachineStates;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IFactionHolder))]
public class AssignmentReceiver : MonoBehaviour, ISelectable, IStateMachineTarget {

	//public delegate void OnAssignmentReceivedDelegate(IAssignmentTarget assignmentTarget);
	//public event OnAssignmentReceivedDelegate OnAssignmentReceived;

	[SerializeField] private List<ObjectAssignmentPair> assignmentDictionary;

	[SerializeField] private StateMachine stateMachine;

	public IAssignmentTarget CurrentAssignment { get; private set; }

	private IFactionHolder factionHolder;

	private void Awake() {
		factionHolder = GetComponent<IFactionHolder>();
	}

	private void Start() {
		GetComponentInChildren<StateMachine>().Initialize(this);// Should not be here (temporary solution)
	}

	public void Select(Faction faction) {
		Debug.Log("Selected " + transform.name);
	}

	public void Assign(Faction faction, IAssignmentTarget assignmentTarget) {
		throw new NotImplementedException();
	}

	public void Assign(Faction assigningFaction, AssignmentTargetType assignmentType, IAssignmentTarget assignmentTarget = null) {
		FactionMatch factionMatch = GetFactionMatch(assigningFaction);
		foreach(ObjectAssignmentPair assignmentPair in assignmentDictionary) {
			if (assignmentPair.CanAssign(factionMatch, assignmentType)) {
				stateMachine.EnterState(assignmentPair.State, assignmentTarget, true);
				return;
			}
		}

		Debug.Log("Unable to assign: " + assignmentType.name);
	}

	private FactionMatch GetFactionMatch(Faction otherFaction) {
		if (otherFaction == factionHolder.Faction) { return FactionMatch.Friend; }
		return FactionMatch.Enemy;
	}

}