using UnityEngine;

[CreateAssetMenu(menuName = "Identifiers/Parent Object Category", fileName = "ParentCategory", order = 50)]
public class ObjectParentCategory : ObjectCategory {

	[SerializeField] private ObjectCategory[] containedCategories;

	public override bool EqualsOrContains(ObjectCategory other) {
		if (other == null) { return false; }
		if (other == this) { return true; }

		foreach (ObjectCategory category in containedCategories) {
			if (category.EqualsOrContains(other)) {
				return true;
			}
		}

		return false;
	}

}