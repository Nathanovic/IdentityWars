using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour {

	[AssetDropdown("Settings/Resources/ObjectCategories/Items", false)] 
	[SerializeField] private ObjectCategory[] itemCategories;
	[SerializeField] private ItemWidget resourceWidgetTemplate;
	[SerializeField] private bool showEmptyItems;

	private Inventory inventory;
	private Dictionary<ObtainableObject, ItemWidget> resourceWidgets;
	private ObtainableObject[] availableItems;

	private void Awake() {
		resourceWidgetTemplate.gameObject.SetActive(false);

		if (itemCategories == null || itemCategories.Length < 1) {
			Debug.LogError("Inventory UI will not work: no item categories specified");
		}
	}

	public void Initialize(Inventory inventory) {
		availableItems = ItemLoader.Instance.GetItemsOfCategory<ObtainableObject>(itemCategories);
		this.inventory = inventory;
		resourceWidgets = new Dictionary<ObtainableObject, ItemWidget>();
		inventory.OnChanged += UpdateVisuals;

		foreach (ObtainableObject item in availableItems) {
			//bool isItemOfDesiredCategory = useAllItemCategories || itemCategories.Contains(item.Category);
			//if (!isItemOfDesiredCategory) { continue; }

			ItemWidget newWidget = Instantiate(resourceWidgetTemplate, resourceWidgetTemplate.transform.parent);
			newWidget.transform.name = "Widget-" + item.name;
			newWidget.Initialize(item.Icon);
			resourceWidgets.Add(item, newWidget);

			newWidget.SetActive(showEmptyItems);
		}
	}

	public void UpdateVisuals() {
		foreach(KeyValuePair<ObtainableObject, ItemWidget> widgetKVP in resourceWidgets) {
			int amount = inventory.ContainedAmount(widgetKVP.Key);
			widgetKVP.Value.UpdateAmount(amount);

			bool showWidget = showEmptyItems || amount != 0;
			widgetKVP.Value.SetActive(showWidget);
		}
	}

}