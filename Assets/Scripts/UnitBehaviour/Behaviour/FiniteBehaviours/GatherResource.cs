using UnityEngine;

public class GatherResource : FiniteBehaviour {
	
	private DepletableResource resource;
	private Inventory inventory;
	private float gatherSpeed;
	private float startTime;

	public GatherResource(DepletableResource resource, Inventory inventory, float gatherSpeed = 1f) {
		this.resource = resource;
		this.inventory = inventory;
		this.gatherSpeed = gatherSpeed;
		startTime = Time.time;
	}

	public override void Update() {
		float time = Time.time - startTime;
		float gatherDuration = resource.GatherDuration / gatherSpeed;

		if (time > gatherDuration) {
			CollectResource();
			startTime = Time.time;
		}
	}

	private void CollectResource() {
		InventoryItem collectedResource = resource.GatherResource();
		if (collectedResource == null) {
			Finish();
			return;
		}

		if (resource.RemainingResources <= 0) {
			Finish();
			return;
		}

		inventory.Add(collectedResource);
		if (inventory.RemainingSpace == 0) {
			Finish();
			return;
		}
	}

}