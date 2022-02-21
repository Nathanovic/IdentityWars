using UnityEngine;

namespace StateMachineStates {
	public class AIGatherResourceState : StateWithData<DepletableResource>, IFactionKnowledgeable {

		[SerializeField] private AIDefaultState defaultState;
		[SerializeField] private MoveRigidbody rigidbodyMover;
		[SerializeField] private float gatherSpeed;

		private FactionKnowledge factionKnowledge;
		private DepletableResource targetResource;
		private ResourceDeliveryPoint targetDeliveryPoint;
		private TriggerListener triggerListener;
		private Inventory inventory;
		private FiniteBehaviourQueue behaviourQueue;

		public override void Initialize(StateMachine stateMachine, IStateMachineTarget target) {
			base.Initialize(stateMachine, target);
			Rigidbody rigidbody = target.GetComponent<Rigidbody>();
			rigidbodyMover.InitializeRigidbody(rigidbody);
			triggerListener = target.GetComponent<TriggerListener>();
			inventory = target.GetComponent<Inventory>();
		}

		public void InitializeFactionKnowledge(FactionKnowledge factionKnowledge) {
			this.factionKnowledge = factionKnowledge;
		}

		protected override void OnEnter(DepletableResource resource) {
			targetResource = resource;
			StartGatheringResource(resource);
		}

		private void StartGatheringResource(DepletableResource resource) {
			targetResource = resource;
			behaviourQueue = new FiniteBehaviourQueue(//TODO: remove behaviourQueue
					new MoveToPosition(rigidbodyMover, resource.TargetPosition, ReachedTargetResource),
					new GatherResource(targetResource, inventory, gatherSpeed)
				);
			behaviourQueue.Start(MoveToDeliveryPoint);
		}

		protected override void OnUpdateRun() {
			/*if (targetResource.RemainingResources <= 0 and we're moving to the resource) { 
				if (inventory.HasItem()) {
					MoveToDeliveryPoint();
					return;
				}
				else {
					TryCollectingNearbyResource();
					return;
				}
			}*/
			behaviourQueue.Update();
		}

		protected override void OnFixedUpdateRun() {
			behaviourQueue.FixedUpdate();
		}

		private void MoveToDeliveryPoint() {
			targetDeliveryPoint = factionKnowledge.GetClosest<ResourceDeliveryPoint>(rigidbodyMover.CurrentPosition);
			if (targetDeliveryPoint == null) {
				Debug.LogWarning("There is no delivery point to deliver my resources to!");
				stateMachine.EnterState(defaultState);
				return;
			}

			behaviourQueue = new FiniteBehaviourQueue(
				new MoveToPosition(rigidbodyMover, targetDeliveryPoint.transform.position, ReachedDepositPoint)
				// new DeliverResources()
				);
			behaviourQueue.Start(DeliverResources);
		}

		private void DeliverResources() {
			Debug.Log("We have arrived at the deposit point!");
			targetDeliveryPoint.DeliverResources(inventory);

			if (targetResource.RemainingResources <= 0) {
				TryCollectingNearbyResource();
				return;
			}

			StartGatheringResource(targetResource);
		}

		private void TryCollectingNearbyResource() {//TODO: make implementation
			stateMachine.EnterState(defaultState);
		}

		private bool ReachedTargetResource() {
			return triggerListener.IsIntersectingWithCollider(targetResource.Collider);
		}

		private bool ReachedDepositPoint() {
			return triggerListener.IsIntersectingWithCollider(targetDeliveryPoint.Collider);
		}

	}
}