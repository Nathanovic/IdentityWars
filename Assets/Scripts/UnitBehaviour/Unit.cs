using UnityEngine;

[SelectionBase]
public class Unit : MonoBehaviour, IStateMachineTarget, IFactionHolder {

	public Vector3 WorldPosition { get { return transform.position; } }
	public Faction Faction { get; private set; }
	public FactionKnowledge FactionKnowledge { get; private set; }
	public UnitSkillSet SkillSet { get { return skillSet; } }

	[SerializeField] private Animator animator;
	[SerializeField] private StateMachine stateMachine;
	[SerializeField] private UnitSkillSet skillSet;

	private void Start() {
		IUnitBehaviour[] myBehaviours = GetComponentsInChildren<IUnitBehaviour>();
		foreach(IUnitBehaviour behaviour in myBehaviours) {
			behaviour.Initialize(animator, this);
		}

		if (stateMachine != null) {
			stateMachine.Initialize(this);
		}
	}

	public void Initialize(Faction faction, FactionKnowledge factionKnowledge) {
		Faction = faction;
		FactionKnowledge = factionKnowledge;
	}

}