using UnityEngine;

public enum AssignmentType {
	None = 0,
	GatherResource = 1,
	Attack = 2
}
public interface IAssignmentTarget {
	public Vector3 Position { get; }
	public AssignmentType AssignmentType { get; }
}

public interface IMoveToAssignment : IAssignmentTarget { }

public interface IAttackableAssignment : IAssignmentTarget { }