using UnityEngine;

public class MoveToTarget : MoveRigidbody <Transform> {

	private Transform target;

	protected override void OnStart(Transform targetTransform) {
		target = targetTransform;
	}

	protected override void OnFixedUpdate() {
		Vector3 direction = target.position - CurrentPosition;
		direction.y = 0f;
		direction = direction.normalized;
		AddForce(direction);
	}

}