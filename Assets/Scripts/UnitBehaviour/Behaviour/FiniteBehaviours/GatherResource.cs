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

			if (CanContinueCollecting()) {
				startTime = Time.time;
			}
			else { 
				Finish();
			}
		}
	}

	private void CollectResource() {
		InventoryItem collectedResource = resource.GatherResource();
		if (collectedResource == null) {
			Debug.LogWarning("There is no resource to gain? Finishing gather behaviour...");
			Finish();
			return;
		}

		inventory.Add(collectedResource);
	}

	private bool CanContinueCollecting() { 
		if (inventory.RemainingSpace <= 0) { return false; }
		if (resource.RemainingResources <= 0) { return false; }
		return true;
	}

}