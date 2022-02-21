using UnityEngine;

public class PlayerInitializer : MonoBehaviour, IStateMachineTarget, IFactionHolder {

	public Faction Faction { get; private set; }
	public Vector3 WorldPosition { get { return transform.position; } }

	[SerializeField] private StateMachine stateMachine;

	private void Start() {
		stateMachine.Initialize(this);
	}

	public void SetFaction(Faction faction) {
		Faction = faction;
	}

	public new T GetComponent<T>() {
		return base.GetComponent<T>();
	}

}