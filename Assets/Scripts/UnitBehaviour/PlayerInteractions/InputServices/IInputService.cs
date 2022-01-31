using UnityEngine;

public interface IInputService {
	public InputMaster.PlayerActions InputActions { get; }
	public Vector2 GetPointerPosition();
}