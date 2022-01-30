using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable variable/Vector2", fileName = "Vector2Variable", order = 100)]
public sealed class ScriptableVector2 : ScriptableVariable<ScriptableVector2, Vector2> {

	[SerializeField] private Vector2 value;

	public override Vector2 DefaultValue {
		get {
			return value;
		}
	}

}