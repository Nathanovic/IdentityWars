using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class FactionInitializer : MonoBehaviour {

	[SerializeField] private Faction faction;
	[SerializeField] private FactionResourcesUI resourcesUI;

	private Inventory inventory;

	private void Awake() {
		inventory = GetComponent<Inventory>();
		Resource[] availableResources = Resources.LoadAll<Resource>("InventoryItems");
		resourcesUI.Initialize(inventory, availableResources);
		transform.name = "Faction_" + faction.ToString();
	}

}