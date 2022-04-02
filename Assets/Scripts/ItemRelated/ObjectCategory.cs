using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Identifiers/Object Category", fileName = "ObjectCategory", order = 50)]
public class ObjectCategory : ScriptableObject, IEquatable<ObjectCategory> { 

	public virtual bool Equals(ObjectCategory other) {
		Debug.Log("Equals check on category is working!");
		return other == this;
	}

}