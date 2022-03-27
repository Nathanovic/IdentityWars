﻿using UnityEngine;

[SelectionBase]
public class Unit : MonoBehaviour, IStateMachineTarget, IFactionHolder {

	public Faction Faction { get; private set; }
	public Vector3 WorldPosition { get { return transform.position; } }
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

	public void SetFaction(Faction faction) {
		Faction = faction;
	}

}