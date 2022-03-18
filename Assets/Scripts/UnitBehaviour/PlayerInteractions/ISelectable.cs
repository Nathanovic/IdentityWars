public interface ISelectable {
	void Select(Faction faction);
	void Deselect();
	void Assign(Faction assigningFaction, AssignmentTargetType assignmentType, IAssignmentTarget assignmentTarget = null);
}