using UnityEngine;

[CreateAssetMenu(menuName = "Obtainable objects/Unit", fileName = "Unit", order = 50)]
public class UnitShopItem : ShopItem {

	[SerializeField] private Unit prefab;

	public Unit Prefab { get { return prefab; } }

}