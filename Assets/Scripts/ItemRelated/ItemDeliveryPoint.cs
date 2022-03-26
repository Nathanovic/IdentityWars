using StateMachineStates;
using UnityEngine;

[SelectionBase]
public class ItemDeliveryPoint : MonoBehaviour, IFactionKnowledgeable, IFactionObject, IStateData {

	public Vector3 WorldPosition { get { return transform.position; } }
	public Collider Collider { get { return interactionCollider; } }

	private Inventory factionInventory;

	[AssetDropdown("Settings/Resources/ItemCategories", false)]
	[SerializeField] private ItemCategory[] itemCategories;
	[SerializeField] private Collider interactionCollider;

	public void DeliverItems(Inventory inventory) {
		inventory.TransferAllTo(factionInventory, itemCategories);
	}

	public void InitializeFactionKnowledge(FactionKnowledge factionKnowledge) {
		factionInventory = factionKnowledge.Inventory;
	}

}