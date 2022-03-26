using Sirenix.OdinInspector;
using UnityEngine;

namespace StateMachineStates {
	public class AIDeliverResourceState : StateWithData<ItemDeliveryPoint> {

		[SerializeField, Required] private MoveToTarget moveToPositionBehaviour;
		[SerializeField, Required] private DeliverItems deliverBehaviour;

		private TriggerListener triggerListener;
		private Inventory inventory;

		private ItemDeliveryPoint deliveryPoint;

		public override void Initialize(StateMachine stateMachine, IStateMachineTarget target) {
			base.Initialize(stateMachine, target);
			triggerListener = target.GetComponent<TriggerListener>();
			inventory = target.GetComponent<Inventory>();
		}

		protected override void OnEnter(ItemDeliveryPoint deliveryPoint) {
			this.deliveryPoint = deliveryPoint;
			moveToPositionBehaviour.StartBehaviour(deliveryPoint.transform);
		}

		protected override void OnUpdateRun() {
			if (!moveToPositionBehaviour.IsActive) { return; }
			bool targetReached = triggerListener.IsIntersectingWithCollider(deliveryPoint.Collider);
			if (targetReached) {
				moveToPositionBehaviour.StopBehaviour();
				DeliverResources();
			}
		}

		private void DeliverResources() {
			DeliverItems.DeliverItemData behaviourData = new DeliverItems.DeliverItemData(inventory, deliveryPoint, OnFinishedDelivery);
			deliverBehaviour.StartBehaviour(behaviourData);
		}

		private void OnFinishedDelivery() {
			stateMachine.ContinueQueue();
		}

	}
}