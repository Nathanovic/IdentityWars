using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemWidget : MonoBehaviour {

	[SerializeField] private Button buyButton;

	private ShopItem shopItem;
	private Action<ShopItem> onButtonBuyCallback;

	private void Awake() {
		gameObject.SetActive(false);
	}

	public void Show(ShopItem item, bool interactable, Action<ShopItem> onButtonBuy) {
		shopItem = item;
		onButtonBuyCallback = onButtonBuy;
		buyButton.interactable = interactable;
		gameObject.SetActive(true);
	}

	public void Hide() {
		gameObject.SetActive(false);
	}

	public void Button_OnBuy() {
		onButtonBuyCallback?.Invoke(shopItem);
	}

}