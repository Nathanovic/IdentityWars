using System;
using UnityEngine;

public class GatherResource : UnitBehaviour<GatherResource.GatherResourceData> {

	public class GatherResourceData {
		public DepletableResource ResourcePile { get; private set; }
		public Resource Resource { get { return ResourcePile.ResourceType; } }
		public Action OnDoneCallback { get; private set; }

		public GatherResourceData(DepletableResource resource, Action onDone) {
			ResourcePile = resource;
			OnDoneCallback = onDone;
		}
	}

	[AssetDropdown("Settings/Resources/InventoryItems")]
	[SerializeField] private Resource resourceType;

	private Inventory inventory;
	private GatherResourceData behaviourData;

	private float startTime;

	public void InitializeInventory(Inventory inventory) {
		this.inventory = inventory;
	}

	public bool CanGather(Resource resourceType) {
		return this.resourceType == resourceType;
	}

	protected override void OnStart(GatherResourceData data) {
		behaviourData = data;
		animator.SetBool("gather", true);
		StartGatheringNextResource();
	}

	protected override void OnUpdate() {
		float time = Time.time - startTime;
		float gatherDuration = behaviourData.Resource.GatherDuration / skillValue;

		if (time > gatherDuration) {
			CollectResource();

			if (CanContinueCollecting()) {
				StartGatheringNextResource();
			}
			else {
				Finish();
			}
		}
	}

	private void StartGatheringNextResource() {
		startTime = Time.time;
	}

	private void CollectResource() {
		InventoryItem collectedResource = behaviourData.ResourcePile.GatherResource();
		if (collectedResource == null) {
			Finish();
			return;
		}

		inventory.Add(collectedResource);
	}

	private bool CanContinueCollecting() {
		if (inventory.RemainingSpace <= 0) { return false; }
		if (behaviourData.ResourcePile.RemainingResources <= 0) { return false; }
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