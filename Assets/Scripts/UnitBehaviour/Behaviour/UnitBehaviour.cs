using UnityEngine;

// Set execution order so all behaviours can execute before the states go and check whether the job is finished
[DefaultExecutionOrder(-1)]
public abstract class UnitBehaviour<T> : MonoBehaviour, IUnitBehaviour {

	public bool IsActive { get; private set; }
	protected Animator animator { get; private set; }

	public void Initialize(Animator animator, Transform transform) {
		this.animator = animator;
		OnInitialize(transform);
	}

	public void Start(T data) {
		IsActive = true;
		OnStart(data);
	}

	public void Stop() {
		IsActive = false;
		OnStop();
	}

	private void Update() {
		if (!IsActive) { return; }
		OnUpdate();
	}
	private void FixedUpdate() {
		if (!IsActive) { return; }
		OnFixedUpdate();
	}

	protected virtual void OnInitialize(Transform transform) { }
	protected abstract void OnStart(T data);// Potentially later on add OnStart without data and make both optional
	protected virtual void OnStop() { }
	protected virtual void OnUpdate() { }
	protected virtual void OnFixedUpdate() { }

}