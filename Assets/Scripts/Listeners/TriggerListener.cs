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

}