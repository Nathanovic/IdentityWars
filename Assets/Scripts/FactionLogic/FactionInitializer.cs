using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class FactionInitializer : MonoBehaviour {

	[SerializeField] private Faction faction;
	[SerializeField] private ShopUI defaultShopUI;

	private FactionObjectBuilder factory;
	private FactionKnowledge factionKnowledge;
	private Inventory inventory;

	private void Awake() {
		inventory = GetComponent<Inventory>();
		factionKnowledge = new FactionKnowledge(inventory);
		factory = new FactionObjectBuilder(transform, OnFactionObjectSpawned);

		transform.name = "Faction_" + faction.ToString();

		// In fact there should be no units or player objects before the game starts, but for (temporary) editor purposes this works
		IFactionHolder[] factionHolders = GetComponentsInChildren<IFactionHolder>();
		foreach (IFactionHolder factionHolder in factionHolders) {
			factionHolder.Initialize(faction, factionKnowledge);
		}

		IFactionObject[] factionObjects = GetComponentsInChildren<IFactionObject>();
		foreach (IFactionObject factionObject in factionObjects) {
			factionKnowledge.AddFactionObject(factionObject);
		}

		Shop[] shops = GetComponentsInChildren<Shop>();
		foreach (Shop shop in shops) {
			InitializeShop(shop);
		}
	}

	public void InitializeShop(Shop shop) {
		shop.Initialize(factory, defaultShopUI);
	}

	private void OnFactionObjectSpawned(GameObject newFactionObject) {
		IFactionHolder factionHolder = newFactionObject.GetComponent<IFactionHolder>();
		if (factionHolder != null) {
			factionHolder.Initialize(faction, factionKnowledge);

			// FactionHolder is always FactionObject
			factionKnowledge.AddFactionObject(factionHolder);
			return;
		}

		IFactionObject factionObject = newFactionObject.GetComponent<IFactionObject>();
		if (factionObject != null) {
			factionKnowledge.AddFactionObject(factionObject);
		}
	}

}