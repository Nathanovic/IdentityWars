using UnityEngine;

[CreateAssetMenu(menuName = "Inventory items/Simple", fileName = "InventoryItem", order = 50)]
public class InventoryItem : ScriptableObject {

	public Sprite Icon { get { return icon; } }
	public ItemCategory Category { get { return category; } }

	[SerializeField] private Sprite icon;
	[AssetDropdown("Settings/Resources/ItemCategories")] [SerializeField] private ItemCategory category;

}