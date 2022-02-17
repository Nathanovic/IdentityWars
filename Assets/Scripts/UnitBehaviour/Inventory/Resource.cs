using UnityEngine;

[CreateAssetMenu(menuName = "Inventory items/Resource", fileName = "Resource", order = 100)]
public class Resource : InventoryItem {

	public float GatherDuration { get { return gatherDuration; } }

	[SerializeField] private float gatherDuration;

}