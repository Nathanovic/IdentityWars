using UnityEngine;

public abstract class Widget : MonoBehaviour {

	public void SetActive(bool active) {
		gameObject.SetActive(active);
	}

}