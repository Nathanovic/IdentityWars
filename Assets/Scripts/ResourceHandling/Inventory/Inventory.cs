using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory : MonoBehaviour {

	public int RemainingSpace { get; private set; }
	public event Action OnChanged;

	[SerializeField] private int maxSpace;

	private Dictionary<InventoryItem, int> containedItems;

	private void Awake() {
		RemainingSpace = maxSpace;
		containedItems = new Dictionary<InventoryItem, int>();
	}

	public void Add(InventoryItem item) {
		if (containedItems.ContainsKey(item)) {
			containedItems[item] += 1;
		}
		else {
			containedItems.Add(item, 1);
		}

		RemainingSpace--;
		OnChanged?.Invoke();
	}

	public void Remove(InventoryItem item, int count = 1) {
		if (!containedItems.ContainsKey(item)) { return; }

		containedItems[item] -= count;
		RemainingSpace -= count;
		OnChanged?.Invoke();
	}

	public bool Contains(InventoryItem item, int count = 1) {
		if (!containedItems.ContainsKey(item)) { return false; }
		return containedItems[item] >= count;
	}

	public int ContainedAmount(InventoryItem item) {
		if (!containedItems.ContainsKey(item)) { return 0; }
		return containedItems[item];
	}

	public void RemoveAll() {
		containedItems.Clear();
		RemainingSpace = maxSpace;

		OnChanged?.Invoke();
	}

}