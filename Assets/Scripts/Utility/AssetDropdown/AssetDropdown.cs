﻿using System;
using UnityEngine;

public class AssetDropdown : PropertyAttribute
{

	public string AssetLocation { get; private set; }
	public bool ShowTitle { get; private set; }
	public Type AssetType { get; private set; }

	public AssetDropdown(string assetLocation, bool showTitle = true, Type assetType = null)
	{
		AssetLocation = assetLocation;
		ShowTitle = showTitle;
		AssetType = assetType;
	}

}