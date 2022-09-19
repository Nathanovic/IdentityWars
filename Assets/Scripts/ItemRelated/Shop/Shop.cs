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
	[SerializeField] private ObjectCategory[] itemCategories;

	private FactionObjectBuilder factory;
	private Inventory inventory;
	private ShopUI ui;

	public void Initialize(FactionObjectBuilder builder, Inventory inventory, ShopUI shopUI) {// Action<ShopItem> onUpgradeBought) {
		factory = builder;
		this.inventory = inventory;
		ui = shopUI;
		ShopItem[] itemsForSale = CategorizedObjectLibrary.Instance.GetObjects<ShopItem>(itemCategories);
		Debug.Log("This shop (" + transform.name + ") sales: " + itemsForSale.Length + " items. First = " + itemsForSale[0].name);
	}

	public void Open() {

		//ui.InitializeShopItems(itemsForSale);
	}

	public void Close() {

	}

}