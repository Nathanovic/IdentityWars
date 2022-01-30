using UnityEngine;
using System;

[Serializable]
public class MoveRigidbody {

	[AssetDropdown("Settings/ScriptableVariables")]
	[SerializeField] private ScriptableInt movementSpeed;

	private Rigidbody rigidbody;

	public void InitializeRigidbody(Rigidbody rb) {
		rigidbody = rb;
	}

	public void AddForce(Vector3 input) {
		Vector3 movementForce = input * movementSpeed.DefaultValue;
		rigidbody.AddForce(movementForce);
	}

}