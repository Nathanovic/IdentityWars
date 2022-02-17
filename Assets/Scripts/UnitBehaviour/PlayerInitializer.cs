using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour, IStateMachineTarget, IFactionHolder {

	public Faction Faction { get { return faction; } }

	[SerializeField] private Faction faction;
	[Required]
	[SerializeField] private StateMachine stateMachine;

	private void Start() {
		stateMachine.Initialize(this);
	}

}