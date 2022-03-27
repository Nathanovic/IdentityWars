using UnityEngine;

public abstract class MoveRigidbody<T> : UnitBehaviour<T> {

	public Vector3 CurrentPosition { get { return transform.position; } }

	public void AddForce(Vector3 input) {
		Vector3 movementForce = input * skillValue;
		animator.SetFloat("moveSpeed", movementForce.magnitude);

		rigidbody.AddForce(movementForce);

		if (rigidbody.velocity.magnitude > 0.1f) {
			Vector3 lookToPosition = transform.position + rigidbody.velocity;
			lookToPosition = new Vector3(lookToPosition.x, 0f, lookToPosition.z);
			transform.LookAt(lookToPosition);
		}
	}

	protected override void OnStop() {
		animator.SetFloat("moveSpeed", 0f);
	}

}