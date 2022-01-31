using UnityEngine;

public class KeyboardInputService : IInputService {

	public InputMaster.PlayerActions InputActions { get; private set; }

	public KeyboardInputService(InputMaster inputMaster) {
		InputActions = inputMaster.Player;
	}

	public Vector2 GetPointerPosition() {
		return InputActions.MousePosition.ReadValue<Vector2>();
	}

}