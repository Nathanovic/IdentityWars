using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class Inventory : MonoBehaviour {

	public int RemainingSpace { get; private set; }
	public event Action OnChanged;

	[SerializeField] private int maxSpace;
	[StarTooltip("Optional: assign an InventoryUI that will draw the inventory contents")]
	[SerializeField] private InventoryUI ui;

	private Dictionary<ObtainableObject, int> containedItems;

	private void Awake() {
		RemainingSpace = maxSpace;
		containedItems = new Dictionary<ObtainableObject, int>();
		if (ui != null) {
			ui.Initialize(this);
		}
	}

	public void TransferAllTo(Inventory targetInventory, params ObjectCategory[] itemCategories) {
		Dictionary<ObtainableObject, int> removeableItems = new Dictionary<ObtainableObject, int>();
		foreach (KeyValuePair<ObtainableObject, int> itemKVP in containedItems) {
			bool isMatchingCategory = false;
			foreach(ObjectCategory category in itemCategories) {
				if (itemKVP.Key.Category == category) {
					isMatchingCategory = true;
					break;
				}
			}
			if (!isMatchingCategory) { break; }

			targetInventory.Add(itemKVP.Key, itemKVP.Value);
			removeableItems.Add(itemKVP.Key, itemKVP.Value);
		}

		foreach (KeyValuePair<ObtainableObject, int> itemKVP in removeableItems) {
			Remove(itemKVP.Key, itemKVP.Value);
		}
	}

	public void Add(ObtainableObject item, int amount = 1) {
		if (containedItems.ContainsKey(item)) {
			containedItems[item] += amount;
		}
		else {
			containedItems.Add(item, amount);
		}

		RemainingSpace -= amount;
		OnChanged?.Invoke();
	}

	public void Remove(ObtainableObject item, int amount = 1) {
		if (!containedItems.ContainsKey(item)) { return; }

		containedItems[item] -= amount;
		RemainingSpace += amount;
		OnChanged?.Invoke();
	}

	public bool Contains(ObtainableObject item, int amount = 1) {
		if (!containedItems.ContainsKey(item)) { return false; }
		return containedItems[item] >= amount;
	}

	public int ContainedAmount(ObtainableObject item) {
		if (!containedItems.ContainsKey(item)) { return 0; }
		return containedItems[item];
	}

	public bool HasItem() {
		foreach (KeyValuePair<ObtainableObject, int> itemKVP in containedItems) {
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