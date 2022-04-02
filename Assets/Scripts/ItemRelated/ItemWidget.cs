using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemWidget : MonoBehaviour {

	[SerializeField] private TMP_Text amountText;
	[SerializeField] private Image resourceIcon;

	public void Initialize(Sprite icon) {
		resourceIcon.sprite = icon;
		amountText.text = "0";
	}

	public void SetActive(bool active) {
		gameObject.SetActive(active);
	}

	public void UpdateAmount(int amount) {
		amountText.text = amount.ToString();
	}

}