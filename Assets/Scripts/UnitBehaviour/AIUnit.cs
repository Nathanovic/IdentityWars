using UnityEngine;

public class AIUnit : MonoBehaviour, IFactionHolder, IStateMachineTarget {

	public Faction Faction { get; private set; }
	public Vector3 WorldPosition { get { return transform.position; } }

	public void SetFaction(Faction faction) {
		Faction = faction;
	}

	public new T GetComponent<T>() {
		return base.GetComponent<T>();
	}

}