using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StarTooltip))]
public class StarTooltipDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		StarTooltip tooltip = (StarTooltip)attribute;
		string propertyName = char.ToUpper(property.displayName[0]) + property.displayName.Substring(1) + "*";
		GUIContent content = new GUIContent(propertyName, tooltip.Text);
		EditorGUI.PropertyField(position, property, content);
	}
}