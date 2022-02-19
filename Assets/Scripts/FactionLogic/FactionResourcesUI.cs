using UnityEngine;
using System.Collections.Generic;

public class FactionResourcesUI : MonoBehaviour {

	[SerializeField] private ResourceWidget resourceWidgetTemplate;

	private Inventory inventory;
	private Dictionary<Resource, ResourceWidget> resourceWidgets;

	private void Awake() {
		resourceWidgetTemplate.gameObject.SetActive(false);
	}

	public void Initialize(Inventory inventory, Resource[] availableResources) {
		this.inventory = inventory;
		resourceWidgets = new Dictionary<Resource, ResourceWidget>();

		resourceWidgetTemplate.gameObject.SetActive(true);

		foreach (Resource resource in availableResources) {
			ResourceWidget newWidget = Instantiate(resourceWidgetTemplate, resourceWidgetTemplate.transform.parent);
			newWidget.transform.name = "Widget-" + resource.name;
			newWidget.Initialize(resource.Icon);
			resourceWidgets.Add(resource, newWidget);
		}

		resourceWidgetTemplate.gameObject.SetActive(false);
		inventory.OnChanged += UpdateVisuals;
	}

	public void UpdateVisuals() {
		foreach(KeyValuePair<Resource, ResourceWidget> widgetKVP in resourceWidgets) {
			int amount = inventory.ContainedAmount(widgetKVP.Key);
			widgetKVP.Value.UpdateAmount(amount);
		}
	}

}