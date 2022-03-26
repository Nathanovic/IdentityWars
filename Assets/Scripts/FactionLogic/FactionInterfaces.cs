using UnityEngine;

public interface IFactionObject {
	public Vector3 WorldPosition { get; }
}

public interface IFactionHolder : IFactionObject {
	public void SetFaction(Faction faction);
	public Faction Faction { get; }
}

public interface IFactionKnowledgeable {
	public void InitializeFactionKnowledge(FactionKnowledge factionKnowledge);
}