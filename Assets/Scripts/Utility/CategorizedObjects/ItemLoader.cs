using System.Collections.Generic;
using UnityEngine;

public class ItemLoader : MonoSingleton<ItemLoader> {

	private ObtainableObject[] allObtainableItems;
	private Dictionary<ObjectCategory, ObtainableObject[]> itemLibrary;

	private void Awake() {
		allObtainableItems = Resources.LoadAll<ObtainableObject>("ObtainableObjects");
		itemLibrary = new Dictionary<ObjectCategory, ObtainableObject[]>();
	}

	public T[] GetItemsOfCategory<T>(params ObjectCategory[] categories) where T : ObtainableObject {
		List<ObtainableObject> items = new List<ObtainableObject>();
		foreach (ObjectCategory category in categories) {
			bool isNewCategory = !itemLibrary.ContainsKey(category);
			if (isNewCategory) {
				LoadItemsIntoLibrary(category);
			}

			items.AddRange(itemLibrary[category]);
		}

		int itemCount = items.Count;
		T[] castedItems = new T[itemCount];
		for (int i = 0; i < itemCount; i++) {
			T castedItem = (T)items[i];
			if (castedItem == null) {
				Debug.LogWarning("Cant cast item " + items[i].Name + " to type: " + typeof(T).ToString(), items[i]);
			}

			castedItems[i] = castedItem;
		}

		return castedItems;
	}

	private void LoadItemsIntoLibrary(ObjectCategory category) {
		if (itemLibrary.ContainsKey(category)) { return; }

		List<ObtainableObject> items = new List<ObtainableObject>();
		foreach (ObtainableObject item in allObtainableItems) {
			if (category.EqualsOrContains(item.Category)) {
				items.Add(item);
			}
		}

		itemLibrary.Add(category, items.ToArray());
	}

}
