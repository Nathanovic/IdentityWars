using UnityEngine;

public class MoveToPosition : FiniteBehaviour {

	private BooleanDelegate targetReached;
	private MoveRigidbody moveRigidbody;
	private Vector3 targetPosition;

	public MoveToPosition(MoveRigidbody moveRigidbody, Vector3 position, BooleanDelegate hasReachedDestination) {
		this.moveRigidbody = moveRigidbody;
		targetPosition = position;
		targetReached = hasReachedDestination;
	}

	public override void FixedUpdate() {
		Vector3 direction = targetPosition - moveRigidbody.CurrentPosition;
		direction.y = 0f;
		direction = direction.normalized;
		moveRigidbody.AddForce(direction);

		if (targetReached()) {
			Finish();
		}
	}

}