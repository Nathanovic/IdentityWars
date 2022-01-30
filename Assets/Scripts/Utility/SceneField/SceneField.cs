using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

[Serializable]
public class SceneField {

	public string SceneName { get { return name; } }

	[SerializeField] private string name;
#if UNITY_EDITOR
	[SerializeField] private Object scene;
#endif

	public static implicit operator string(SceneField sceneField) {
		if(sceneField == null) { return null; }
		return sceneField.name;
	}

	public bool Equals(Scene scene) {
		string[] splittedName = name.Split('/');
		string sceneName = splittedName[splittedName.Length - 1];
		Debug.Log(sceneName);
		return sceneName.Equals(scene.name);
	}

}