using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AssetDropdown))]
public class AssetDropdownDrawer : PropertyDrawer
{

	private const int SELECT_BUTTON_WIDTH = 15;
	private const int GUI_ITEM_X_OFFSET = 3;
	private const double ASSET_REFRESH_TIME = 4;

	private double previousCacheTime = -100;
	private Object[] dropdownObjects;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		position.width -= (SELECT_BUTTON_WIDTH + GUI_ITEM_X_OFFSET);
		float dropdownWidth = position.width;

		AssetDropdown assetDropdown = (AssetDropdown)attribute;
		UpdateDropdownObjects(fieldInfo.FieldType, assetDropdown);

		List<string> options = new List<string>();
		int index = 0;
		int selectedIndex = -1;

		string currentSelectedName = property.objectReferenceValue != null ? property.objectReferenceValue.name : string.Empty;

		foreach (Object obj in dropdownObjects)
		{
			string name = obj == null ? "None" : obj.name;
			options.Add(name);

			if ((obj == null && string.IsNullOrEmpty(currentSelectedName)) ||
				(obj != null && obj.name == currentSelectedName))
				selectedIndex = index;

			index++;
		}

		int newSelectedIndex;
		if (assetDropdown.ShowTitle)
			newSelectedIndex = EditorGUI.Popup(position, property.displayName, selectedIndex, options.ToArray());
		else
			newSelectedIndex = EditorGUI.Popup(position, selectedIndex, options.ToArray());

		if (dropdownObjects.Length <= 1)
			return;

		if (newSelectedIndex != selectedIndex)
		{
			Object newSelected = dropdownObjects[newSelectedIndex];
			property.objectReferenceValue = newSelected;
		}

		bool objectSelected = dropdownObjects[newSelectedIndex] != null;
		if (objectSelected)
		{
			position.x += dropdownWidth + GUI_ITEM_X_OFFSET;
			position.width = SELECT_BUTTON_WIDTH;
			Texture pingIcon = EditorGUIUtility.Load("Icons/pingObject.png") as Texture;
			if (GUI.Button(position, pingIcon, "label"))
				EditorGUIUtility.PingObject(dropdownObjects[newSelectedIndex]);
		}
	}

	private void UpdateDropdownObjects(System.Type fieldType, AssetDropdown assetDropdown)
	{
		double timeSincePreviousLoad = EditorApplication.timeSinceStartup - previousCacheTime;
		if (timeSincePreviousLoad < ASSET_REFRESH_TIME)
			return;

		System.Type fieldBaseType = GetFieldBaseType(fieldType);
		List<Object> objects = new List<Object>();

		string[] guids = AssetDatabase.FindAssets("t:" + fieldBaseType.ToString(), new[] { "Assets/" + assetDropdown.AssetLocation });
		foreach (string guid in guids)
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(guid);
			objects.Add(AssetDatabase.LoadAssetAtPath(assetPath, fieldBaseType));
		}

		previousCacheTime = EditorApplication.timeSinceStartup;
		objects.Insert(0, null);
		dropdownObjects = objects.ToArray();
	}

	private System.Type GetFieldBaseType (System.Type fieldType)
	{
		if (fieldType.IsArray)
			return fieldType.GetElementType();

		else if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(List<>))
			return fieldType.GetGenericArguments()[0];

		return fieldType;
	}

}