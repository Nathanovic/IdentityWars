using UnityEngine;

public class StarTooltip : PropertyAttribute {

	public string Text { get; private set; }

	public StarTooltip(string text) {
		Text = text;
	}
	
}