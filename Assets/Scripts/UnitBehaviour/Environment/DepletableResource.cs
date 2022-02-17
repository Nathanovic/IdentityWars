using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DepletableResource : MonoBehaviour, IAssignmentTarget {

	public Vector3 TargetPosition { get { return transform.position; } }
	public Collider Collider { get; private set; }

	public int RemainingResources { get { return resourceCount; } }
	public float GatherDuration { get { return resource.GatherDuration; } }

	[SerializeField] private int resourceCount;
	[SerializeField] private Resource resource;

	public Resource GatherResource() {
		resourceCount--;
		if (resourceCount <= 0) {
			Destroy(gameObject);
		}
		return resource;
	}

	private void Awake() {
		Collider = GetComponent<Collider>();
	}

}