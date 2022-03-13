using UnityEngine;

public abstract class MoveRigidbody<T> : UnitBehaviour<T> {

	public Vector3 CurrentPosition { get { return transform.position; } }

	private new Transform transform;
	private new Rigidbody rigidbody;

	protected override void OnInitialize(Transform transform) {
		this.transform = transform;
		rigidbody = transform.GetComponent<Rigidbody>();
	}

	public void AddForce(Vector3 input, float speed) {
		Vector3 movementForce = input * speed;
		if (animator != null) {
			animator.SetFloat("moveSpeed", movementForce.magnitude);
		}

		rigidbody.AddForce(movementForce);
		
		Vector3 lookToPosition = transform.position + rigidbody.velocity;
		lookToPosition = new Vector3(lookToPosition.x, 0f, lookToPosition.z);
		transform.LookAt(lookToPosition);
	}

	protected override void OnStop() {
		if (animator != null) {
			animator.SetFloat("moveSpeed", 0f);
		}
	}

}