using System;
using UnityEngine;

// TODO: deliver all resources over time instead
public class DeliverItems : UnitBehaviour<DeliverItems.DeliverItemData> {

	public class DeliverItemData {
		public ItemDeliveryPoint DeliveryPoint { get; private set; }
		public Action OnDoneCallback { get; private set; }
		public Inventory Inventory { get; private set; }

		public DeliverItemData(Inventory inventory, ItemDeliveryPoint deliveryPoint, Action onDone) {
			Inventory = inventory;
			DeliveryPoint = deliveryPoint;
			OnDoneCallback = onDone;
		}
	}

	[SerializeField] private float deliverDuration;

	private DeliverItemData behaviourData;
	private float startTime;

	protected override void OnStart(DeliverItemData data) {
		behaviourData = data;
		startTime = Time.time;
		animator.SetBool("deliver", true);
	}

	protected override void OnUpdate() {
		float passedTime = Time.time - startTime;
		if (passedTime >= deliverDuration) {
			behaviourData.DeliveryPoint.DeliverItems(behaviourData.Inventory);
			Stop();
		}
	}

	protected override void OnStop() {
		behaviourData.OnDoneCallback?.Invoke();
		animator.SetBool("deliver", false);
	}

}