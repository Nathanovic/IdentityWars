using System;
using System.Collections.Generic;
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
	[AssetDropdown("Settings/Resources/ItemCategories", false)]
	[SerializeField] private List<ObjectCategory> itemCategories;

	private FactionObjectBuilder factory;
	private ShopUI ui;
	private Action<UnitShopItem, Vector3> onUnitBoughtCallback;

	private ShopItem[] itemsForSale;

	public void Initialize(FactionObjectBuilder builder, ShopUI shopUI) {// Action<ShopItem> onUpgradeBought) {
		factory = builder;
		ui = shopUI;

	}

	public void ShowUI() {
		ui.Show(itemsForSale);
	}

}