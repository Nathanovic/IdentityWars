using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class Inventory : MonoBehaviour {

	public int RemainingSpace { get; private set; }
	public event Action OnChanged;

	[SerializeField] private int maxSpace;
	[SerializeField] private InventoryUI ui;

	private Dictionary<InventoryItem, int> containedItems;

	private void Awake() {
		RemainingSpace = maxSpace;
		containedItems = new Dictionary<InventoryItem, int>();
		if (ui != null) {
			ui.Initialize(this);
		}
	}

	public void TransferAllTo(Inventory targetInventory) {
		TransferAllTo<InventoryItem>(targetInventory);
	}

	public void TransferAllTo<T>(Inventory targetInventory) where T : InventoryItem {
		Dictionary<T, int> removeableItems = new Dictionary<T, int>();
		foreach(KeyValuePair<InventoryItem, int> itemKVP in containedItems) {
			T castedItem = (T)itemKVP.Key;
			if (castedItem == null) { continue; }
			targetInventory.Add(itemKVP.Key, itemKVP.Value);
			removeableItems.Add((T)itemKVP.Key, itemKVP.Value);
		}

		foreach(KeyValuePair<T, int> itemKVP in removeableItems) {
			Remove(itemKVP.Key, itemKVP.Value);
		}
	}

	public void Add(InventoryItem item, int amount = 1) {
		if (containedItems.ContainsKey(item)) {
			containedItems[item] += amount;
		}
		else {
			containedItems.Add(item, amount);
		}

		RemainingSpace -= amount;
		OnChanged?.Invoke();
	}

	public void Remove(InventoryItem item, int amount = 1) {
		if (!containedItems.ContainsKey(item)) { return; }

		containedItems[item] -= amount;
		RemainingSpace += amount;
		OnChanged?.Invoke();
	}

	public bool Contains(InventoryItem item, int amount = 1) {
		if (!containedItems.ContainsKey(item)) { return false; }
		return containedItems[item] >= amount;
	}

	public int ContainedAmount(InventoryItem item) {
		if (!containedItems.ContainsKey(item)) { return 0; }
		return containedItems[item];
	}

	public bool HasItem() {
		foreach (KeyValuePair<InventoryItem, int> itemKVP in containedItems) {
			if (itemKVP.Value > 0) { return true; }
		}

		return false;
	}

	private void Clear() {
		containedItems.Clear();
		RemainingSpace = maxSpace;

		OnChanged?.Invoke();
	}

}