public interface ISelectable {
	void Select(Faction faction);
	void Assign(Faction faction, IAssignmentTarget assignmentTarget);
}