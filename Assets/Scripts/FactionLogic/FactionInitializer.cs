using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class FactionInitializer : MonoBehaviour {

	[SerializeField] private Faction faction;
	
	private FactionKnowledge factionKnowledge;
	private Inventory inventory;

	private void Awake() {
		inventory = GetComponent<Inventory>();
		factionKnowledge = new FactionKnowledge(inventory);

		transform.name = "Faction_" + faction.ToString();

		// In fact there should be no units or player objects before the game starts, but for (temporary) editor purposes this works
		IFactionObject[] factionObjects = GetComponentsInChildren<IFactionObject>();
		foreach (IFactionObject factionObject in factionObjects) {
			factionKnowledge.AddFactionObject(factionObject);
		}

		IFactionHolder[] factionHolders = GetComponentsInChildren<IFactionHolder>();
		foreach (IFactionHolder factionHolder in factionHolders) {
			factionHolder.SetFaction(faction);
		}

		IFactionKnowledgeable[] knowledgeableFactionHolders = GetComponentsInChildren<IFactionKnowledgeable>();
		foreach (IFactionKnowledgeable factionHolder in knowledgeableFactionHolders) {
			factionHolder.InitializeFactionKnowledge(factionKnowledge);
		Shop[] shops = GetComponentsInChildren<Shop>();
		foreach (Shop shop in shops) {
			InitializeShop(shop);
		}
	}

	public void Initialize(GameObject newFactionObject) {
		IFactionObject factionObject = newFactionObject.GetComponent<IFactionObject>();
		if (factionObject != null) {
			factionKnowledge.AddFactionObject(factionObject);
		}
	public void InitializeShop(Shop shop) {
		shop.Initialize(factory, defaultShopUI);
	}

		IFactionHolder factionHolder = newFactionObject.GetComponent<IFactionHolder>();
		if (factionObject != null) {
			factionHolder.SetFaction(faction);
		}

		IFactionKnowledgeable factionKnowledgeable = newFactionObject.GetComponent<IFactionKnowledgeable>();
		if (factionKnowledgeable != null) {
			factionKnowledgeable.InitializeFactionKnowledge(factionKnowledge);
		}
	}

}