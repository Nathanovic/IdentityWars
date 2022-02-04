using UnityEngine;

public enum AssignmentType {
	None = 0,
	GatherResource = 1,
	Attack = 2,
	Move = 3
}
public interface IAssignmentTarget {
	public Vector3 TargetPosition { get; }
	public AssignmentType AssignmentType { get; }
}