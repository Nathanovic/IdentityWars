using System;
using UnityEngine;

[Serializable]
public class RandomValue {
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;

    public float GetRandom() {
        return UnityEngine.Random.Range(minValue, maxValue);
	}
}
