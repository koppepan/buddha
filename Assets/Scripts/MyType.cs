using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class MyType
{
	public static float ObjectToFloat(object data)
	{
		float f = 0;
		if (float.TryParse (data.ToString(), out f)) {
			return f;
		}

		int i = 0;
		if (int.TryParse (data.ToString(), out i)) {
			return (float)i;
		}
		return 0;
	}
}

public struct GameData
{
	public TimeSpan TimeLimit;
	public float HalfPercentage;
	public float BuddhaPercentage;

	public GameData(float TimeLimit, float HalfPercentage, float BuddhaPercentage)
	{
		this.TimeLimit = TimeSpan.FromSeconds(TimeLimit);
		this.HalfPercentage = HalfPercentage;
		this.BuddhaPercentage = BuddhaPercentage;
	}
	public GameData(Dictionary<string, object> data)
	{
		this.TimeLimit = TimeSpan.FromSeconds (MyType.ObjectToFloat (data ["TimeLimit"]));
		this.HalfPercentage = MyType.ObjectToFloat (data ["HalfPercentage"]);
		this.BuddhaPercentage = MyType.ObjectToFloat (data ["BuddhaPercentage"]);
	}
};

