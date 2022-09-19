using System.Collections.Generic;
using UnityEngine;

public class ObjectPool <T> where T : Component {

    private T prefab;

    private List<T> poolItems;
    private Transform defaultParent;

    public ObjectPool(T prefab, Transform parent) {
        this.prefab = prefab;
        poolItems = new List<T>();
		defaultParent = parent;
    }

	public ObjectPool(T prefab) {
		this.prefab = prefab;
		poolItems = new List<T>();
		string poolName = "Pool_" + typeof(T).ToString();
		defaultParent = new GameObject(poolName).transform;
	}

	public T GetItem(bool isUIElement = false, Transform parent = null) {
        T item;

        if (poolItems.Count > 0) {
            item = poolItems[0];
            poolItems.RemoveAt(0);
        }
        else {
            parent = (parent == null) ? defaultParent : parent;
            item = Object.Instantiate(prefab);
        }
        item.transform.SetParent(parent);

        if (isUIElement) {
            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, 0f);
		}
        return item;
    }

    public void HideItem(T item) {
        item.transform.position = new Vector3(0, 0, -1000);
        item.transform.SetParent(defaultParent);
        poolItems.Add(item);
    }

    public void HideItems(IEnumerable<T> items) {
        foreach (T item in items) {
            HideItem(item);
		}
    }

}