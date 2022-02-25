using UnityEngine;

public abstract class GameScreenSingleton<T> : GameScreen where T : GameScreenSingleton<T> {

	private static T instance;
	public static T Instance {
		get {
			if (instance == null) {
				Debug.LogWarning("GameScreenSingleton of type '" + typeof(T).ToString() + "' is not set...");
			}
			return instance;
		}
		private set {
			instance = value;
		}
	}

	public override GameScreenState Initialize() {
		instance = (T)this;
		return base.Initialize();
	}

}