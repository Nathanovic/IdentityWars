using UnityEngine;

[CreateAssetMenu(menuName = "Identifiers/Assignment Target Type", fileName = "AssignmentTargetType", order = 50)]
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