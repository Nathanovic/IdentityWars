using UnityEngine;
using System;

[Serializable]
public class MoveRigidbody {

	public Vector3 CurrentPosition { get { return rigidbody.position; } }

	[SerializeField] private float moveSpeed = 100;

	private Rigidbody rigidbody;

	public void InitializeRigidbody(Rigidbody rb) {
		rigidbody = rb;
	}

	public void AddForce(Vector3 input) {
		Vector3 movementForce = input * moveSpeed;
		rigidbody.AddForce(movementForce);
	}

}