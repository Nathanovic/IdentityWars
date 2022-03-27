using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FactionInitializer))]
public class FactionBuilder : MonoBehaviour {

	[SerializeField] private ShopUI shopUI;

	private FactionInitializer initializer;

	private void Awake() {
		initializer = GetComponent<FactionInitializer>();

		Shop[] shops = GetComponentsInChildren<Shop>();
		foreach(Shop shop in shops) {
			shop.Initialize(shopUI, OnUnitBought, null);
		}
	}

	public void OnUnitBought(ShopItem shopItem, Vector3 position) {
		GameObject newUnit = Instantiate(shopItem.Prefab, position, Quaternion.identity, transform);
		initializer.Initialize(newUnit);
	}

}