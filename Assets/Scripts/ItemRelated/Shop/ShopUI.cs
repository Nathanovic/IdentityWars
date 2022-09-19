using System;
using UnityEngine;

public class ShopUI : GameScreen {

	[SerializeField] private ShopItemWidget widgetTemplate;

	private ObjectPool<ShopItemWidget> widgetsPool;

	private void Awake() {
		widgetsPool = new ObjectPool<ShopItemWidget>(widgetTemplate, transform);
		widgetTemplate.gameObject.SetActive(false);
	}

	public void Activate(ShopItem[] shopItems, Action<ShopItem> onItemBought) {
		foreach (ShopItem item in shopItems) {
			ShopItemWidget newWidget = widgetsPool.GetItem(true);
			newWidget.transform.name = "Widget-" + item.name;
			newWidget.Show(item, true, onItemBought);
		}

		Activate();
	}

}
