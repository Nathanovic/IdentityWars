public interface ISelectable {
	void Select(Faction faction);
	void Assign(Faction assigningFaction, AssignmentTargetType assignmentType, IAssignmentTarget assignmentTarget = null);
}