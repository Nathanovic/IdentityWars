using UnityEngine;

public class MoveToTarget : MoveRigidbody <Transform> {

	[SerializeField] private float defaultSpeed = 100;

	private Transform target;
	private float movementSpeed;

	protected override void OnStart(Transform targetTransform) {
		target = targetTransform;
		movementSpeed = defaultSpeed;
	}

	protected override void OnFixedUpdate() {
		Vector3 direction = target.position - CurrentPosition;
		direction.y = 0f;
		direction = direction.normalized;
		AddForce(direction, movementSpeed);
	}

}