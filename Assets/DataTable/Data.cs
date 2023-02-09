using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class Data : ScriptableObject
{
	public List<Excel> objectdatas; // Replace 'EntityType' to an actual type that is serializable.
	public List<PlayerData> playerData; // Replace 'EntityType' to an actual type that is serializable.
}
