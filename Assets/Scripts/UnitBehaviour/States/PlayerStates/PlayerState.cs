namespace StateMachineStates {
	public abstract class PlayerState : State {

		protected PlayerInitializer target { get; private set; }
		protected PlayerInput input { get; private set; }

		public sealed override void Initialize(StateMachine stateMachine, IStateMachineTarget target) {
			this.target = (PlayerInitializer)target;
			input = this.target.GetComponent<PlayerInput>();
			base.Initialize(stateMachine, target);
		}

	}
}