using UnityEngine;

public class GatherResourceAssignment : MoveAssignment {
	
	public Vector3 ResourcePosition { get { return resource.transform.position; } }

	private Resource resource;

	public GatherResourceAssignment(Resource resource) : base(resource.transform.position) {
		this.resource = resource;
		AssignmentType = AssignmentType.GatherResource;
	}

}


namespace StateMachineStates {
	public class GatherResourcesState {
		public class GatherResourcesStateData {
			public Resource resource;
		}


	}
}