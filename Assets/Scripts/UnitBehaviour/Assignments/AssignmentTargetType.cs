using UnityEngine;

[CreateAssetMenu(fileName = "AssignmentTargetType", order = 100)]
public class AssignmentTargetType : ScriptableObject {

	[SerializeField] private AssignmentTargetType[] containedTypes;

	public bool EqualsOrContains(AssignmentTargetType other) {
		if (other == this) { return true; }

		foreach(AssignmentTargetType type in containedTypes) {
			return type.EqualsOrContains(other);
		}

		return false;
	}
}