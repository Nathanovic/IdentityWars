using Sirenix.OdinInspector;
using UnityEngine;

namespace StateMachineStates {
	public class AIGatherResourceState : StateWithData<DepletableResource>, IFactionKnowledgeable {

		[SerializeField, Required] private AIDefaultState defaultState;
		[SerializeField, Required] private AIDeliverResourceState deliverResourceState;
		[SerializeField, Required] private MoveToTarget moveToPositionBehaviour;
		[SerializeField, Required] private GatherResource gatherBehaviour;
		
		private FactionKnowledge factionKnowledge;
		private TriggerListener triggerListener;
		private Inventory inventory;

		private DepletableResource targetResource;
		private ItemDeliveryPoint targetDeliveryPoint;

		public override void Initialize(StateMachine stateMachine, IStateMachineTarget target) {
			base.Initialize(stateMachine, target);
			triggerListener = target.GetComponent<TriggerListener>();
			inventory = target.GetComponent<Inventory>();
		}

		public void InitializeFactionKnowledge(FactionKnowledge factionKnowledge) {
			this.factionKnowledge = factionKnowledge;
		}

		protected override void OnEnter(DepletableResource resource) {
			targetResource = resource;
			if (inventory.RemainingSpace > 0) {
				MoveToResource(resource);
			}
			else {
				MoveToDeliveryPoint();
			}
		}

		protected override void OnExit() {
			gatherBehaviour.Stop();
			moveToPositionBehaviour.Stop();
			base.OnExit();
		}

		protected override void OnUpdateRun() {
			if (moveToPositionBehaviour.IsActive) {
				if (targetResource.RemainingResources <= 0) {
					moveToPositionBehaviour.Stop();
					TryCollectingNearbyResource();
					return;
				}

				bool reachedTargetResource = triggerListener.IsIntersectingWithCollider(targetResource.Collider);
				if (reachedTargetResource) {
					moveToPositionBehaviour.Stop();
					GatherTargetResource();
				}
			}
		}

		private void MoveToResource(DepletableResource resource) {
			targetResource = resource;
			moveToPositionBehaviour.Start(resource.transform);
		}

		private void GatherTargetResource() {
			GatherResource.GatherResourceData gatherResourceData = new GatherResource.GatherResourceData(targetResource, OnFinishedCollecting);
			gatherBehaviour.Start(gatherResourceData);
		}

		private void MoveToDeliveryPoint() {
			targetDeliveryPoint = factionKnowledge.GetClosest<ItemDeliveryPoint>(moveToPositionBehaviour.CurrentPosition);
			if (targetDeliveryPoint == null) {
				Debug.LogWarning("There is no delivery point to deliver my resources to!", transform);
				stateMachine.EnterState(defaultState);
				return;
			}

			stateMachine.QueueState(deliverResourceState, targetDeliveryPoint);
			stateMachine.QueueState(this, targetResource);
			stateMachine.ContinueQueue();
		}

		//TODO: make implementation
		private void TryCollectingNearbyResource() {
			stateMachine.EnterState(defaultState);
		}

		private void OnFinishedCollecting() {
			if (inventory.RemainingSpace > 0) {
				TryCollectingNearbyResource();
			}
			else {
				MoveToDeliveryPoint();
			}
		}

	}
}