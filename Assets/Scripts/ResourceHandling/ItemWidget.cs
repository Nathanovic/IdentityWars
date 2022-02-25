using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemWidget : Widget {

	[SerializeField] private TMP_Text amountText;
	[SerializeField] private Image resourceIcon;

	public void Initialize(Sprite icon) {
		resourceIcon.sprite = icon;
		amountText.text = "0";
	}

	public void UpdateAmount(int amount) {
		amountText.text = amount.ToString();
	}

}