using System;
using UnityEngine;

public class Shop : MonoBehaviour {

	// TODO:
	// 1 Think out code architecture between gameState, shop, inventory and items that should be spawned 
	// 2 Code logic to disable movement but allow UI things
	// 3 Build Super simple shop screen that can be opened
	// 4 Design how upgrades should be supported
	// 5 Make Scriptable Object logic for purchaseable items (units first)
	// 6 Test & iterate!

	[SerializeField] private Transform spawnPosition;

	private ShopUI ui;
	private Action<ShopItem, Vector3> onObjectBoughtCallback;
	private Action<ShopItem> onUpgradeBoughtCallback;

	//private ShopItem[]

	public void Initialize(ShopUI shopUI, Action<ShopItem, Vector3> onObjectBought, Action<ShopItem> onUpgradeBought) {
		ui = shopUI;
		onObjectBoughtCallback = onObjectBought;
		onUpgradeBoughtCallback = onUpgradeBought;
	}

	private void OnItemSelected(ShopItem shopItem) {
		if (shopItem.Type == ShopItem.ItemType.Unit) {
			onObjectBoughtCallback?.Invoke(shopItem, spawnPosition.position);
		}
	}

}