using UnityEngine;

public class TimeScaler : MonoBehaviour {

	public float slowTimeScale = 0.3f;
	public float quickTimeScale = 2f;
	public KeyCode slowTimeKey;
	public KeyCode quickTimeKey;

	private void Awake() {
#if !UNITY_EDITOR
		Destroy(gameObject);
#endif
	}

	void Update () {
		if (UnityEngine.Input.GetKeyDown(slowTimeKey)) {
			Time.timeScale = slowTimeScale;
		} else if (UnityEngine.Input.GetKeyDown(quickTimeKey)) {
			Time.timeScale = quickTimeScale;
		}

		if (UnityEngine.Input.GetKeyUp(slowTimeKey)) {
			Time.timeScale = 1f;
		} else if (UnityEngine.Input.GetKeyUp(quickTimeKey)) {
			Time.timeScale = 1f;
		}
	}

}
