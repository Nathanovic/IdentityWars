public interface ISelectable {
	void Select(Faction faction);
	IAssignmentTarget Interact();
	void Assign(Faction faction, IAssignmentTarget assignmentTarget);
}