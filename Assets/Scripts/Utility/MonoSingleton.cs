using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> {

	private static T instance;

	public static T Instance {
		get {
			if (instance == null) {
				GameObject instanceObject = new GameObject();
				instanceObject.name = typeof(T).ToString() + "_Singleton";
				instance = instanceObject.AddComponent<T>();
			}
			return instance;
		}
	}

	protected virtual void Initialize() { }

}