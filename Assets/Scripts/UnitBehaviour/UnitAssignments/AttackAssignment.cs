using UnityEngine;

public class AttackAssignment : IAssignmentTarget {
	public Vector3 TargetPosition => throw new System.NotImplementedException();
	public AssignmentType AssignmentType { get { return AssignmentType.Attack; } }

}