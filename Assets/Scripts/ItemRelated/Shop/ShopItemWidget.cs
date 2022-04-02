using System;
using UnityEngine;

public class ShopItemWidget : MonoBehaviour {

	private ShopItem shopItem;
	private Action<ShopItem> onButtonBuyCallback;

	private void Awake() {
		gameObject.SetActive(false);
	}

	public void Show(ShopItem item, Action<ShopItem> onButtonBuy) {
		gameObject.SetActive(true);
		shopItem = item;
		onButtonBuyCallback = onButtonBuy;
	}

	public void Button_OnBuy() {
		onButtonBuyCallback?.Invoke(shopItem);
	}

}