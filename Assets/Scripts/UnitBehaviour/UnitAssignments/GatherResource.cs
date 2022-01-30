using UnityEngine;

public class GatherResource : IAssignmentTarget {
	
	public Vector3 Position { get { return resource.transform.position; } }
	public AssignmentType AssignmentType { get { return AssignmentType.GatherResource; } }

	private Resource resource;

	public GatherResource(Resource resource) {
		this.resource = resource;
	}

}