using UnityEngine;

public abstract class PriorityBasedState : MonoBehaviour {

	protected PriorityBasedStateMachine stateMachine { get; private set; }

	public virtual void Initialize(PriorityBasedStateMachine stateMachine, IStateMachineTarget target) {
		this.stateMachine = stateMachine;
	}
	public virtual void Activate() { }
	public virtual void Deactivate() { }
	public abstract void Run();
	public virtual void FixedUpdateRun() { }
	public virtual bool CanStart() {
		return true;
	}

}