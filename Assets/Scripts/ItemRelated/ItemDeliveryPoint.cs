using StateMachineStates;
using UnityEngine;

[SelectionBase]
public class ItemDeliveryPoint : MonoBehaviour, IFactionHolder, IStateData {

	public Vector3 WorldPosition { get { return transform.position; } }
	public Faction Faction { get; private set; }
	public Collider Collider { get { return interactionCollider; } }

	private Inventory factionInventory;

	[AssetDropdown("Settings/Resources/ObjectCategories/Items", false)]
	[SerializeField] private ObjectCategory[] itemCategories;
	[SerializeField] private Collider interactionCollider;

	public void DeliverItems(Inventory inventory) {
		inventory.TransferAllTo(factionInventory, itemCategories);
	}

	public void Initialize(Faction faction, FactionKnowledge factionKnowledge) {
		Faction = faction;
		factionInventory = factionKnowledge.Inventory;
	}

}