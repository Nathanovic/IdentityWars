using UnityEngine;
using System;

[Serializable]
public class SkillValue {

	public Skill Skill { get { return skill; } }
	public int Value { get { return value; } }

	[AssetDropdown("Settings/Skills")] [SerializeField] private Skill skill;
	[SerializeField] private int value;

}