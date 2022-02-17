using UnityEngine;

namespace StateMachineStates {
	public class AIGatherResourceState : StateWithData<DepletableResource> {

		[SerializeField] private AIDefaultState defaultState;
		[SerializeField] private MoveRigidbody rigidbodyMover;
		[SerializeField] private float gatherSpeed;

		private DepletableResource targetResource;
		private TriggerListener triggerListener;
		private Inventory inventory;
		private FiniteBehaviourQueue behaviourQueue;
		private Vector3 depositPoint;

		protected override void OnInitialize() {
			Rigidbody rigidbody = GetComponentInParent<Rigidbody>();
			rigidbodyMover.InitializeRigidbody(rigidbody);
			triggerListener = rigidbody.GetComponent<TriggerListener>();
			inventory = rigidbody.GetComponent<Inventory>();
		}

		protected override void OnEnter(DepletableResource resource) {
			targetResource = resource;
			depositPoint = transform.position;//Temp!
			StartGatheringResource(resource);
		}

		private void StartGatheringResource(DepletableResource resource) {
			targetResource = resource;
			behaviourQueue = new FiniteBehaviourQueue(
					new MoveToPosition(rigidbodyMover, resource.TargetPosition, ReachedTargetResource),
					new GatherResource(targetResource, inventory, gatherSpeed),
					new MoveToPosition(rigidbodyMover, transform.position, ReachedDepositPoint)
				);
			behaviourQueue.Start(OnArrivedAtDeposit);
		}

		protected override void OnUpdateRun() {
			behaviourQueue.Update();
		}

		protected override void OnFixedUpdateRun() {
			behaviourQueue.FixedUpdate();
		}

		private void OnArrivedAtDeposit() {
			Debug.Log("We have arrived at the deposit point!");
			stateMachine.EnterState(defaultState);
		}

		private bool ReachedTargetResource() {
			return triggerListener.IsIntersectingWithCollider(targetResource.Collider);
		}

		private bool ReachedDepositPoint() {
			return Vector3.Distance(rigidbodyMover.CurrentPosition, depositPoint) < 1.5f;
		}

	}
}