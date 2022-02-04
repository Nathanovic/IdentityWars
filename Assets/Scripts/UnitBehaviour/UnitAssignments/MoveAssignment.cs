using UnityEngine;

public class MoveAssignment : IAssignmentTarget {
	
	public Vector3 TargetPosition { get; protected set; }
	public AssignmentType AssignmentType { get; protected set; }

	public MoveAssignment(Vector3 position) {
		TargetPosition = position;
		AssignmentType = AssignmentType.Move;
	}

}