using System;
using UnityEngine;

public class GatherResource : UnitBehaviour<GatherResource.GatherResourceData> {

	public class GatherResourceData {
		public DepletableResource Resource { get; private set; }
		public Action OnDoneCallback { get; private set; }

		public GatherResourceData(DepletableResource resource, Action onDone) {
			Resource = resource;
			OnDoneCallback = onDone;
		}
	}

	[SerializeField] private float gatherSpeed;
	[SerializeField] private Inventory inventory;

	private GatherResourceData behaviourData;

	private float startTime;

	protected override void OnStart(GatherResourceData data) {
		behaviourData = data;
		startTime = Time.time;
		animator.SetBool("gather", true);
	}

	protected override void OnUpdate() {
		float time = Time.time - startTime;
		float gatherDuration = behaviourData.Resource.GatherDuration / gatherSpeed;

		if (time > gatherDuration) {
			CollectResource();

			if (CanContinueCollecting()) {
				startTime = Time.time;
			}
			else {
				Finish();
			}
		}
	}

	private void CollectResource() {
		InventoryItem collectedResource = behaviourData.Resource.GatherResource();
		if (collectedResource == null) {
			Finish();
			return;
		}

		inventory.Add(collectedResource);
	}

	private bool CanContinueCollecting() {
		if (inventory.RemainingSpace <= 0) { return false; }
		if (behaviourData.Resource.RemainingResources <= 0) { return false; }
		return true;
	}

	private void Finish() {
		behaviourData.OnDoneCallback?.Invoke();
		StopBehaviour();
	}

	protected override void OnStop() {
		animator.SetBool("gather", false);
	}

}