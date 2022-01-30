using UnityEngine;

public class Resource : MonoBehaviour, IInteractable, ISelectable {

	public void Select(Faction _) {
		Debug.Log("Selecting resource: " + transform.name);
	}

	public IAssignmentTarget Interact() {
		Debug.Log("Interacting with resource " + transform.name);
		return new GatherResource(this);
	}

}