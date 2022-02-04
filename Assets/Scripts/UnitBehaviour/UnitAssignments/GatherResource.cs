using UnityEngine;

public class GatherResource : MoveAssignment {
	
	public Vector3 ResourcePosition { get { return resource.transform.position; } }

	private Resource resource;

	public GatherResource(Resource resource) : base(resource.transform.position) {
		this.resource = resource;
		AssignmentType = AssignmentType.GatherResource;
	}

}