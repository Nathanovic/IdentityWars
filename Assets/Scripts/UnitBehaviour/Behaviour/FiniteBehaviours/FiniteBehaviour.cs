using System;

public abstract class FiniteBehaviour {

	public delegate bool BooleanDelegate();

	private Action onDoneCallback;

	public virtual void Update() { }
	public virtual void FixedUpdate() { }

	public void Start(Action onDone) {
		onDoneCallback = onDone;
	}

	protected void Finish() {
		onDoneCallback?.Invoke();
	}

}