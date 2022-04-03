using System.Collections.Generic;
using UnityEngine;

public class CategorizedObjectLibrary : MonoSingleton<CategorizedObjectLibrary> {

	private ObtainableObject[] allObtainableObjects;
	private Dictionary<ObjectCategory, ObtainableObject[]> library;

	private void Awake() {
		allObtainableObjects = Resources.LoadAll<ObtainableObject>("ObtainableObjects");
		library = new Dictionary<ObjectCategory, ObtainableObject[]>();
	}

	public T[] GetObjects<T>(params ObjectCategory[] categories) where T : ObtainableObject {
		if (categories.Length == 0) {
			Debug.LogWarning("Trying to get objects without category, this is not supported!");
			return null;
		}

		List<ObtainableObject> items = new List<ObtainableObject>();
		foreach (ObjectCategory category in categories) {
			bool isNewCategory = !library.ContainsKey(category);
			if (isNewCategory) {
				LoadItemsIntoLibrary(category);
			}

			items.AddRange(library[category]);
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
		if (library.ContainsKey(category)) { return; }

		List<ObtainableObject> items = new List<ObtainableObject>();
		foreach (ObtainableObject item in allObtainableObjects) {
			if (category.EqualsOrContains(item.Category)) {
				items.Add(item);
			}
		}

		library.Add(category, items.ToArray());
	}

}
