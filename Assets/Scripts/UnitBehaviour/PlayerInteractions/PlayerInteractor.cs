using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerInteractor {

	private Faction faction;
	private InputMaster.PlayerActions input;

	private List<ISelectable> selectedThings;

	public PlayerInteractor(InputMaster.PlayerActions playerInput, Faction playerFaction) {
		input = playerInput;
		faction = playerFaction;
		selectedThings = new List<ISelectable>();
	}

	public void Enable() {
		input.Select.performed += OnSelectKeyDown;
		input.InteractKey.performed += OnInteractKeyDown;
	}

	public void Disable() {
		input.Select.performed -= OnSelectKeyDown;
		input.InteractKey.performed -= OnInteractKeyDown;
	}

	private void OnSelectKeyDown(InputAction.CallbackContext context) {
		Ray mouseRay = Camera.main.ScreenPointToRay(input.MousePosition.ReadValue<Vector2>());
		if (Physics.Raycast(mouseRay, out RaycastHit hit)) {
			if (hit.collider != null) {
				Debug.Log("HIt collider: " + hit.collider.name);
				selectedThings.Clear();
				ISelectable selectable = hit.collider.GetComponent<ISelectable>();
				if (selectable != null) {
					selectedThings.Add(selectable);
					selectable.Select(faction);
				}
			}
		}
	}

	private void OnInteractKeyDown(InputAction.CallbackContext context) {
		Debug.Log("Interact key down!");
	}

}