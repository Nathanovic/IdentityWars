using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Identifiers/Object Category", fileName = "ObjectCategory", order = 50)]
public class ObjectCategory : ScriptableObject { 

	public virtual bool EqualsOrContains(ObjectCategory other) {
		return other == this;
	}

}