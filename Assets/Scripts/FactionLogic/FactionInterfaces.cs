using UnityEngine;

public interface IFactionObject {
	public Vector3 WorldPosition { get; }
}

public interface IFactionHolder : IFactionObject {
	public void Initialize(Faction faction, FactionKnowledge factionKnowledge);
	public Faction Faction { get; }
}