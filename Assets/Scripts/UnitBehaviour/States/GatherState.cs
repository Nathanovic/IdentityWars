using UnityEngine;

namespace StateMachineStates {
	public abstract class GatherState : StateWithData<DepletableResource> {

		[SerializeField] private State defaultState;
		[SerializeField] private GatherResource[] gatherBehaviours;

		protected Inventory inventory { get; private set; }
		protected DepletableResource targetResource;
		protected GatherResource gatherBehaviour { get; private set; }

		public override void Initialize(StateMachine stateMachine, IStateMachineTarget target) {
			base.Initialize(stateMachine, target);
			inventory = target.GetComponent<Inventory>();
			foreach (GatherResource behaviour in gatherBehaviours) {
				behaviour.InitializeInventory(inventory);
			}
		}

		protected sealed override void OnEnter(DepletableResource resource) {
			if (resource == null || resource.RemainingResources <= 0) {
				Debug.LogWarning("Entered GatherState without available resource. Aborting process...", this);
				stateMachine.EnterState(defaultState);
				return;
			}

			gatherBehaviour = null;
			foreach (GatherResource behaviour in gatherBehaviours) {
				if (!behaviour.gameObject.activeInHierarchy) { continue; }
				if (behaviour.CanGather(resource.ResourceType)) {
					gatherBehaviour = behaviour;
					break;
				}
			}

			if (gatherBehaviour == null) {
				// TODO: add visual feedback that the skill is missing and remove this warning
				Debug.LogWarning("Entered GatherState without the required skill. Aborting process...", this);
				EnterDefaultState();
				return;
			}

			targetResource = resource;
			OnEnter();
		}

		protected new abstract void OnEnter();

		protected void EnterDefaultState() {
			stateMachine.EnterState(defaultState);
		}

	}
}