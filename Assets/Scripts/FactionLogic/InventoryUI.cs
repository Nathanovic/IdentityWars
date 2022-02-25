using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class InventoryUI : MonoBehaviour {

	[SerializeField] private bool useAllItemCategories;
	[HideIf("useAllItemCategories")]
	[AssetDropdown("Settings/Resources/ItemCategories", false)] 
	[SerializeField] private List<ItemCategory> itemCategories;
	[SerializeField] private ItemWidget resourceWidgetTemplate;
	[SerializeField] private bool showEmptyItems;

	private Inventory inventory;
	private Dictionary<InventoryItem, ItemWidget> resourceWidgets;
	private InventoryItem[] availableItems;

	private void Awake() {
		resourceWidgetTemplate.gameObject.SetActive(false);

		if (itemCategories == null || itemCategories.Count < 1) {
			Debug.LogError("Inventory UI will not work: no item categories specified");
		}
	}

	public void Initialize(Inventory inventory) {
		availableItems = Resources.LoadAll<InventoryItem>("InventoryItems");
		this.inventory = inventory;
		resourceWidgets = new Dictionary<InventoryItem, ItemWidget>();
		inventory.OnChanged += UpdateVisuals;

		foreach (InventoryItem item in availableItems) {
			bool isItemOfDesiredCategory = useAllItemCategories || itemCategories.Contains(item.Category);
			if (!isItemOfDesiredCategory) { continue; }

			ItemWidget newWidget = Instantiate(resourceWidgetTemplate, resourceWidgetTemplate.transform.parent);
			newWidget.transform.name = "Widget-" + item.name;
			newWidget.Initialize(item.Icon);
			resourceWidgets.Add(item, newWidget);

			newWidget.SetActive(showEmptyItems);
		}
	}

	public void UpdateVisuals() {
		foreach(KeyValuePair<InventoryItem, ItemWidget> widgetKVP in resourceWidgets) {
			int amount = inventory.ContainedAmount(widgetKVP.Key);
			widgetKVP.Value.UpdateAmount(amount);

			bool showWidget = showEmptyItems || amount != 0;
			widgetKVP.Value.SetActive(showWidget);
		}
	}

}