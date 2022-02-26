using UnityEngine;

[SelectionBase]
public class Unit : MonoBehaviour, IStateMachineTarget, IFactionHolder {

	public Faction Faction { get; private set; }
	public Vector3 WorldPosition { get { return transform.position; } }

	[SerializeField] private StateMachine stateMachine;

	private void Start() {
		if (stateMachine != null) {
			stateMachine.Initialize(this);
		}
	}

	public void SetFaction(Faction faction) {
		Faction = faction;
	}

}