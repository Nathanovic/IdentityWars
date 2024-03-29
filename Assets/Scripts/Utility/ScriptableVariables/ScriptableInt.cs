﻿using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable variable/Int", fileName = "IntVariable", order = 100)]
public sealed class ScriptableInt : ScriptableVariable<ScriptableInt, int> {

	[SerializeField] private int value;

	public override int DefaultValue {
		get {
			return value;
		}
	}

}