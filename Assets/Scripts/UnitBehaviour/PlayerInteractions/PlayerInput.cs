using UnityEngine;

public class PlayerInput : MonoBehaviour {

	public InputMaster.PlayerActions PlayerActions { get { return inputService.InputActions; } }

	private IInputService inputService;

	private void Awake() {
		InputMaster inputMaster = new InputMaster();
		inputService = new KeyboardInputService(inputMaster);
		PlayerActions.Enable();
	}

	public void Disable() {

	}

	public Vector2 GetPointerPosition() {
		return inputService.GetPointerPosition();
	}

}