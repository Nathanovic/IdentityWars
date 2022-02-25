using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour {

	[AssetDropdown("Settings/Resources/ItemCategories", false)] [SerializeField] private List<ItemCategory> itemCategories;
	[SerializeField] private ItemWidget resourceWidgetTemplate;

	private Inventory inventory;
	private Dictionary<InventoryItem, ItemWidget> resourceWidgets;

	private void Awake() {
		resourceWidgetTemplate.gameObject.SetActive(false);

		if (itemCategories == null || itemCategories.Count < 1) {
			Debug.LogError("Inventory UI will not work: no item categories specified");
		}
	}

	public void Initialize(Inventory inventory) {
		InventoryItem[] availableItems = Resources.LoadAll<InventoryItem>("InventoryItems");
		this.inventory = inventory;
		resourceWidgets = new Dictionary<InventoryItem, ItemWidget>();

		resourceWidgetTemplate.gameObject.SetActive(true);

		foreach (InventoryItem item in availableItems) {
			bool isItemOfDesiredCategory = itemCategories.Contains(item.Category);
			if (!isItemOfDesiredCategory) { continue; }

			ItemWidget newWidget = Instantiate(resourceWidgetTemplate, resourceWidgetTemplate.transform.parent);
			newWidget.transform.name = "Widget-" + item.name;
			newWidget.Initialize(item.Icon);
			resourceWidgets.Add(item, newWidget);
		}

		resourceWidgetTemplate.gameObject.SetActive(false);
		inventory.OnChanged += UpdateVisuals;
	}

	public void UpdateVisuals() {
		foreach(KeyValuePair<InventoryItem, ItemWidget> widgetKVP in resourceWidgets) {
			int amount = inventory.ContainedAmount(widgetKVP.Key);
			widgetKVP.Value.UpdateAmount(amount);
		}
	}

}