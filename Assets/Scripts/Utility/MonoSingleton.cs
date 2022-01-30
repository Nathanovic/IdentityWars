using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> {

	private static T instance;

	public static T Instance {
		get {
			if (instance == null) {
				GameObject instanceObject = new GameObject();
				instanceObject.name = "Input";
				instance = instanceObject.AddComponent<T>();
				instance.Initialize();
			}
			return instance;
		}
	}

	protected abstract void Initialize();

}