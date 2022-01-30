namespace StateMachineStates {
	public abstract class PlayerState : State {

		protected PlayerController target { get; private set; }

		public sealed override void Initialize(StateMachine stateMachine, IStateMachineTarget target) {
			this.target = (PlayerController)target;
			base.Initialize(stateMachine, target);
		}

	}
}