using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Scriptable variable/PlayerInput", fileName = "PlayerInput", order = 100)]
public sealed class ScriptablePlayerInput : ScriptableVariable<ScriptablePlayerInput, InputMaster.PlayerActions> {

	private InputMaster.PlayerActions value;
	[NonSerialized]private bool isInitialized = false;

	public override InputMaster.PlayerActions DefaultValue {
		get {
			if (!isInitialized) {
				Initialize();
			}
			return value;
		}
	}

	private void Initialize() {
		value = Input.Instance.InputMaster.Player;
		value.Enable();
		isInitialized = true;
	}

}