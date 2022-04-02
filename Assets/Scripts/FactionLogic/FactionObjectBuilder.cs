using System;
using UnityEngine;

public class FactionObjectBuilder {

	// TODO: add object pooling

	private Transform activeObjectsParent;

	private Action<GameObject> onFactionObjectSpawnedCallback;

	public FactionObjectBuilder(Transform activeParent, Action<GameObject> onFactionObjectSpawned) {
		activeObjectsParent = activeParent;
		onFactionObjectSpawnedCallback = onFactionObjectSpawned;
	}

	public void SpawnUnit(Unit unitPrefab, Vector3 position) {
		Unit newUnit = GameObject.Instantiate(unitPrefab, position, Quaternion.identity, activeObjectsParent);
		onFactionObjectSpawnedCallback(newUnit.gameObject);
	}

}