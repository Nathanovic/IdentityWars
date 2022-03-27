using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DepletableResource : MonoBehaviour, IAssignmentTarget {

	public Collider Collider { get; private set; }
	public Resource ResourceType { get { return resource; } }

	public int RemainingResources { get { return resourceCount; } }

	[SerializeField] private int resourceCount;
	[SerializeField] private Resource resource;
	[SerializeField] private GameObject filledVisualsAndCollider;

	public Resource GatherResource() {
		if (resourceCount <= 0) { return null; }
		resourceCount--;
		filledVisualsAndCollider.SetActive(resourceCount > 0);
		return resource;
	}

	private void Awake() {
		Collider = GetComponent<Collider>();
		filledVisualsAndCollider.SetActive(resourceCount > 0);
	}

}