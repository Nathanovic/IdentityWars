using UnityEngine;

public class AIUnit : MonoBehaviour, IFactionHolder {
	public Faction Faction { get { return faction; } }

	[SerializeField] private Faction faction;

}