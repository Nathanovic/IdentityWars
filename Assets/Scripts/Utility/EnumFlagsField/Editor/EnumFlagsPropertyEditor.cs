using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
public class EnumFlagsPropertyEditor : PropertyDrawer {

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		property.intValue = EditorGUI.MaskField(position, property.name, property.intValue, property.enumNames);
	}

}