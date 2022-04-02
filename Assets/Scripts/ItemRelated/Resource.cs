using UnityEngine;

[CreateAssetMenu(menuName = "Inventory items/Resource", fileName = "Resource", order = 50)]
public class Resource : ObtainableObject {

	public float GatherDuration { get { return gatherDuration; } }

	[SerializeField] private float gatherDuration;

}