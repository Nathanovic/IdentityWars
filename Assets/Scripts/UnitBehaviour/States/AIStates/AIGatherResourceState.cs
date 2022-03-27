using Sirenix.OdinInspector;
using UnityEngine;

namespace StateMachineStates {
	public class AIGatherResourceState : GatherState, IFactionKnowledgeable {

		[SerializeField, Required] private MoveToTarget moveToPositionBehaviour;
		[SerializeField, Required] private AIDeliverResourceState deliverResourceState;

		private FactionKnowledge factionKnowledge;
		private TriggerListener triggerListener;

		private ItemDeliveryPoint targetDeliveryPoint;

		public override void Initialize(StateMachine stateMachine, IStateMachineTarget target) {
			base.Initialize(stateMachine, target);
			triggerListener = target.GetComponent<TriggerListener>();
		}

		public void InitializeFactionKnowledge(FactionKnowledge factionKnowledge) {
			this.factionKnowledge = factionKnowledge;
		}

		protected override void OnEnter() {
			if (inventory.RemainingSpace > 0) {
				MoveToResource(targetResource);
			}
			else {
				MoveToDeliveryPoint();
			}
		}

		protected override void OnExit() {
			if (gatherBehaviour != null) {
				gatherBehaviour.StopBehaviour();
				moveToPositionBehaviour.StopBehaviour();
			}
		}

		protected override void OnUpdateRun() {
			if (moveToPositionBehaviour.IsActive) {
				if (targetResource.RemainingResources <= 0) {
					moveToPositionBehaviour.StopBehaviour();
					TryCollectingNearbyResource();
					return;
				}

				bool reachedTargetResource = triggerListener.IsIntersectingWithCollider(targetResource.Collider);
				if (reachedTargetResource) {
					moveToPositionBehaviour.StopBehaviour();
					GatherTargetResource();
				}
			}
		}

		private void MoveToResource(DepletableResource resource) {
			targetResource = resource;
			moveToPositionBehaviour.StartBehaviour(resource.transform);
		}

		private void GatherTargetResource() {
			GatherResource.GatherResourceData gatherResourceData = new GatherResource.GatherResourceData(targetResource, OnFinishedGathering);
			gatherBehaviour.StartBehaviour(gatherResourceData);
		}

		private void MoveToDeliveryPoint() {
			targetDeliveryPoint = factionKnowledge.GetClosest<ItemDeliveryPoint>(moveToPositionBehaviour.CurrentPosition);
			if (targetDeliveryPoint == null) {
				Debug.LogWarning("There is no delivery point to deliver my resources to!", transform);
				EnterDefaultState();
				return;
			}

			stateMachine.QueueState(deliverResourceState, targetDeliveryPoint);
			stateMachine.QueueState(this, targetResource);
			stateMachine.ContinueQueue();
		}

		//TODO: make implementation
		private void TryCollectingNearbyResource() {
			EnterDefaultState();
		}

		private void OnFinishedGathering() {
			if (inventory.RemainingSpace > 0) {
				TryCollectingNearbyResource();
			}
			else {
				MoveToDeliveryPoint();
			}
		}

	}
}