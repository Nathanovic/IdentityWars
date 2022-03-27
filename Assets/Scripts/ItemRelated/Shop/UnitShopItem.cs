using UnityEngine;

[CreateAssetMenu(menuName = "Shop items/Unit", fileName = "Unit", order = 50)]
public class UnitShopItem : ShopItem<Unit> {

	[SerializeField] private Unit prefab;

	public override Unit Prefab { get { return prefab; } }

}