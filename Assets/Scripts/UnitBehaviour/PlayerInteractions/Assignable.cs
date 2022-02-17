using UnityEngine;

public class Assignable : MonoBehaviour {

	public bool IsAssignable = true;
	public Vector3 TargetPosition { get { return transform.position; } }
	public Faction Faction { 
		get {
			if (factionHolder == null) { return Faction.None; }
			else { return factionHolder.Faction; }
		}
	}
	public IAssignmentTarget AssignmentTarget { get; private set; }
	public AssignmentTargetType AssignmentTargetType { get { return assignmentTargetType; } }

	[SerializeField] private AssignmentTargetType assignmentTargetType;

	private IFactionHolder factionHolder;

	private void Awake() {
		factionHolder = GetComponent<IFactionHolder>();
		AssignmentTarget = GetComponent<IAssignmentTarget>();
	}

}