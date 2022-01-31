using UnityEngine;

public class Resource : MonoBehaviour, IInteractable, ISelectable {

	public void Select(Faction _) {
		Debug.Log("Selecting resource: " + transform.name);
	}

	public IAssignmentTarget Interact() {
		Debug.Log("Interacting with resource " + transform.name);
		return new GatherResource(this);
	}

	public void Assign(Faction _, IAssignmentTarget assignmentTarget) {
		Debug.Log("I'm just a resource, I won't interact with that silly thing of type " + assignmentTarget.AssignmentType.ToString());
	}

}