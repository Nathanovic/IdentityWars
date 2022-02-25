using UnityEngine;

public abstract class Widget : MonoBehaviour {

	protected void Show() {
		gameObject.SetActive(true);
	}

	public void Hide() {
		gameObject.SetActive(false);
	}

}