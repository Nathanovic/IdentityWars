using StateMachineStates;
using UnityEngine;

public interface IAssignmentTarget : IStateData {
	public Vector3 TargetPosition { get; }
}