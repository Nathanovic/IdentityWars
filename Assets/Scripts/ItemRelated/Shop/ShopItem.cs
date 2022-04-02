using System.Collections.Generic;
using UnityEngine;

public abstract class ShopItem : ObtainableObject {
    
	public class PurchaseCost {
		public Resource Resource { get { return resource; } }
		public int Amound { get { return amount; } }

		[AssetDropdown("Settings/Resources")] [SerializeField] private Resource resource;
		[SerializeField] private int amount;
	}

	public IEnumerable<PurchaseCost> Costs { get { return costs; } }

	[SerializeField] private List<PurchaseCost> costs = new List<PurchaseCost>();

}