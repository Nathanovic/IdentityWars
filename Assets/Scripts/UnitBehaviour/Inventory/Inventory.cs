using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory : MonoBehaviour {

	public int RemainingSpace { get; private set; }

	[SerializeField] private int maxSpace;

	private List<InventoryItem> containedItems;

	private void Awake() {
		RemainingSpace = maxSpace;
		containedItems = new List<InventoryItem>();
	}

	public bool AddItem(InventoryItem item) {
		if (RemainingSpace == 0) { return false; }

		containedItems.Add(item);
		RemainingSpace--;

		return true;
	}

	public IEnumerable<InventoryItem> RemoveAll() {
		List<InventoryItem> inventoryCopy = new List<InventoryItem>(containedItems);
		containedItems.Clear();
		RemainingSpace = maxSpace;

		return inventoryCopy;
	}

}