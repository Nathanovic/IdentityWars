using UnityEngine;

[CreateAssetMenu(menuName = "Inventory items/Resource", fileName = "Resource", order = 100)]
public class Resource : InventoryItem {

	public float GatherDuration { get { return gatherDuration; } }
	public Sprite Icon { get { return icon; } }

	[SerializeField] private float gatherDuration;
	[SerializeField] private Sprite icon;

}