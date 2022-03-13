using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

//I don't want other scripts to be dependent of this script, but I do want states to be able to disable/enable it (do I???)
//QUESTION: should I detach this script more from the other scripts? If so, how?? 
//(I don't like delegates & events)
[RequireComponent(typeof(PlayerInput))]
public class PlayerInteractor : MonoBehaviour {

	[SerializeField] private LayerMask interactableLayerMask;
	[SerializeField] private bool startActive = true;

	private Faction faction;
	private PlayerInput input;

	private List<ISelectable> selectedThings;

	private void Awake() {
		selectedThings = new List<ISelectable>();
		input = GetComponent<PlayerInput>();
		faction = GetComponent<IFactionHolder>().Faction;
	}

	private void Start() {
		if (startActive) {
			Enable();
		}
		else {
			Disable();
		}
	}

	public void Enable() {
		input.PlayerActions.Select.performed += OnSelectKeyDown;
		input.PlayerActions.InteractKey.performed += OnInteractKeyDown;
	}

	public void Disable() {
		input.PlayerActions.Select.performed -= OnSelectKeyDown;
		input.PlayerActions.InteractKey.performed -= OnInteractKeyDown;
	}

	private void OnSelectKeyDown(InputAction.CallbackContext context) {
		Collider selectedCollider = GetColliderAtPointerPosition(input.GetPointerPosition());
		if (selectedCollider != null) {
			selectedThings.Clear();
			ISelectable selectable = selectedCollider.GetComponent<ISelectable>();
			if (selectable != null) {
				selectedThings.Add(selectable);
				selectable.Select(faction);
			}
		}
	}

	private void OnInteractKeyDown(InputAction.CallbackContext context) {
		if (selectedThings.Count == 0) { return; }

		Collider selectedCollider = GetColliderAtPointerPosition(input.GetPointerPosition());
		if (selectedCollider == null) { return; }
		Assignable assignable = selectedCollider.GetComponent<Assignable>();
		if (assignable == null) { return; }
		selectedThings[0].Assign(faction, assignable.AssignmentTargetType, assignable.AssignmentTarget);
	}

	private Collider GetColliderAtPointerPosition(Vector2 pointerPosition) {
		Ray pointerRay = Camera.main.ScreenPointToRay(pointerPosition);
		if (Physics.Raycast(pointerRay, out RaycastHit hit, interactableLayerMask)) {
			return hit.collider;
		}

		return null;
	}

}