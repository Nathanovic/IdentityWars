using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR// Useful for debugging
[Serializable]
#endif
public class FactionKnowledge {

	public Inventory Inventory { get; private set; }

	private List<IFactionObject> factionObjects;

	public FactionKnowledge(Inventory inventory) {
		factionObjects = new List<IFactionObject>();
		Inventory = inventory;
	}

	public void AddFactionObject(IFactionObject holder) {
		factionObjects.Add(holder);
	}

	public void RemoveFactionObject(IFactionObject holder) {
		factionObjects.Remove(holder);
	}

	public T GetClosest<T>(Vector3 position) where T : class, IFactionObject {
		T closest = null;
		float closestDistance = Mathf.Infinity;

		foreach(IFactionObject holder in factionObjects) {
			if (holder.GetType() != typeof(T)) { continue; }
			
			float distance = Vector3.Distance(position, holder.WorldPosition);
			if (distance < closestDistance) {
				closestDistance = distance;
				closest = (T)holder;
			}
		}

		return closest;
	}

}