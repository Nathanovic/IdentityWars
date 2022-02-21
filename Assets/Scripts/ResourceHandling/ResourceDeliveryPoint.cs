using UnityEngine;

public class ResourceDeliveryPoint : MonoBehaviour, IFactionKnowledgeable, IFactionObject {

	public Vector3 WorldPosition { get { return transform.position; } }
	public Collider Collider { get { return interactionCollider; } }

	private Inventory factionInventory;

	[SerializeField] private Collider interactionCollider;

	public void DeliverResources(Inventory inventory) {
		inventory.TransferAllTo<Resource>(factionInventory);
	}

	public void InitializeFactionKnowledge(FactionKnowledge factionKnowledge) {
		factionInventory = factionKnowledge.Inventory;
	}

}