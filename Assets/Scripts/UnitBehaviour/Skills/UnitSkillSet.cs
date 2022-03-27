using System;
using UnityEngine;

[Serializable]
public class UnitSkillSet {

	// A dictionary might be prettier in code, but is not better in the inspector (a lot of work to set up and no nice dropdown as we have now for SkillValue.Skill)
	[SerializeField] private SkillValue[] skills;

	public bool HasSkil(Skill skill) {
		foreach (SkillValue mySkill in skills) {
			if (mySkill.Skill == skill) {
				return true;
			}
		}

		return false;
	}

	public int GetValue(Skill skill) {
		foreach (SkillValue mySkill in skills) {
			if (mySkill.Skill == skill) {
				return mySkill.Value;
			}
		}

		return -1;
	}

}