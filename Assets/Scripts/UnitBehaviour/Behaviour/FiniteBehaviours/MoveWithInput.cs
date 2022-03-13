using UnityEngine;

public class MoveWithInput : MoveRigidbody<MoveWithInput.MoveWithInputData> {

	public class MoveWithInputData {
		public InputDelegate GetInput { get; private set; }
		public float Speed { get; private set; }

		public MoveWithInputData(InputDelegate getInput, float speed) {
			GetInput = getInput;
			Speed = speed;
		}
	}

	public delegate Vector2 InputDelegate();
	private MoveWithInputData behaviourData;

	protected override void OnStart(MoveWithInputData data) {
		behaviourData = data;
	}

	protected override void OnFixedUpdate() {
		Vector2 input = Vector2.ClampMagnitude(behaviourData.GetInput(), 1f);
		Vector3 movement3D = new Vector3(input.x, 0f, input.y);
		AddForce(movement3D, behaviourData.Speed);
	}

}
