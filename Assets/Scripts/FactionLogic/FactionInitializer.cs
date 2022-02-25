using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class FactionInitializer : MonoBehaviour {

	[SerializeField] private Faction faction;
	[SerializeField] private InventoryUI resourcesUI;
	
	private FactionKnowledge factionKnowledge;
	private Inventory inventory;

	private void Awake() {
		inventory = GetComponent<Inventory>();
		factionKnowledge = new FactionKnowledge(inventory);

		resourcesUI.Initialize(inventory);
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
		}
	}

}