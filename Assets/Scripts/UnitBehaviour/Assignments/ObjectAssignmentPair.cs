using System;
using StateMachineStates;
using UnityEngine;

[Serializable]
public class ObjectAssignmentPair {
	public State State { get { return state; } }

	[SerializeField] private AssignmentTargetType assignmentTargetType;
	[SerializeField] private FactionMatch factionMatch;//TODO: make enum FactionMatch instead
	[SerializeField] private State state;

	public bool CanAssign(FactionMatch match, AssignmentTargetType assignmentTargetType) {
		if (assignmentTargetType != this.assignmentTargetType) { return false; }
		if (factionMatch != FactionMatch.None && factionMatch != match) { return false; }

		if (state == null) {
			Debug.LogWarning("Trying to assign " + assignmentTargetType.ToString() + ", but there is no state to execute...");
			return false;
		}

		return true;
	}
}