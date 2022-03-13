using System.Collections.Generic;
using UnityEngine;

public class TriggerListener : MonoBehaviour {

	private List<Collider> intersectingColliders;

	public delegate void ColliderDelegate(Collider other);
	public event ColliderDelegate OnTriggerEntered;
	public event ColliderDelegate OnTriggerExited;

	private void Awake() {
		intersectingColliders = new List<Collider>();
	}

	private void OnTriggerEnter(Collider other) {
		OnTriggerEntered?.Invoke(other);
		intersectingColliders.Add(other);
	}

	private void OnTriggerExit(Collider other) {
		OnTriggerExited?.Invoke(other);
		intersectingColliders.Remove(other);
	}

	public bool IsIntersectingWithCollider(Collider collider) {
		return intersectingColliders.Contains(collider);
	}

	public T[] GetNearbyComponentsOfType<T>() where T : MonoBehaviour {
		List<T> nearbyComponents = new List<T>();
		
		foreach (Collider collider in intersectingColliders) {
			T component = collider.GetComponent<T>();
			if (component != null) {
				nearbyComponents.Add(component);
			}
		}

		return nearbyComponents.ToArray();
	}

	public T GetNearbyComponentOfType<T>(bool preferClosest) where T : MonoBehaviour {
		T[] nearbyComponents = GetNearbyComponentsOfType<T>();
		
		if (nearbyComponents.Length == 0) { return null; }
		if (nearbyComponents.Length == 1 || !preferClosest) { return nearbyComponents[0]; }

		T closestComponent = nearbyComponents[0];
		float closestDistance = Vector3.Distance(transform.position, nearbyComponents[0].transform.position);
		for(int i = 1; i < nearbyComponents.Length; i ++) {
			T component = nearbyComponents[i];
			float distance = Vector3.Distance(transform.position, component.transform.position);
			if (distance < closestDistance) {
				closestDistance = distance;
				closestComponent = component;
			}
		}

		return closestComponent;
	}

}