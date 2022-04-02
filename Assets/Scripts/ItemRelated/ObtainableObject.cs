using UnityEngine;

[CreateAssetMenu(menuName = "Obtainable objects/Simple", fileName = "SimpleObject", order = 50)]
public class ObtainableObject : ScriptableObject {

	public string Name { get { return name; } }
	public Sprite Icon { get { return icon; } }
	public ObjectCategory Category { get { return category; } }

	[SerializeField] private new string name;
	[SerializeField] private Sprite icon;
	[AssetDropdown("Settings/Resources/ObjectCategories")] [SerializeField] private ObjectCategory category;

}