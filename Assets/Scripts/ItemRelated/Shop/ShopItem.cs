using System.Collections.Generic;
using UnityEngine;

public abstract class ShopItem<T> : ScriptableObject {
    
	public class PurchaseCost {
		public Resource Resource { get { return resource; } }
		public int Amound { get { return amount; } }

		[AssetDropdown("Settings/Resources")] [SerializeField] private Resource resource;
		[SerializeField] private int amount;
	}

	public string Name { get { return name; } }
	public Sprite Icon { get { return icon; } }
	public IEnumerable<PurchaseCost> Costs { get { return costs; } }
	public abstract T Prefab { get; }

	[SerializeField] private new string name;
	[SerializeField] private Sprite icon;
	[SerializeField] private List<PurchaseCost> costs = new List<PurchaseCost>();

}