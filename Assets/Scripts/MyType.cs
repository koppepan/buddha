using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
	public float TimeLimit;
	public float HalfPercentage;
	public float BuddhaPercentage;

	public int Day;

	public float StomachStartValue;
	public float HotokeStartValue;
	public float FaithStartValue;

	public float StomachMaxValue;
	public float HotokeMaxValue;
	public float FaithMaxValue;

	public float StomachLostTime;

	public float StomacCoolTime;
	public float TrainingCoolTime;
	public float PreachingCoolTime;

	public float GainStomacValue;
	public float GainHotokeValue;
	public float GainFaithValue;

	public GameData(Dictionary<string, object> data)
	{
		this.TimeLimit = MyType.ObjectToFloat (data ["TimeLimit"]);
		this.HalfPercentage = MyType.ObjectToFloat (data ["HalfPercentage"]);
		this.BuddhaPercentage = MyType.ObjectToFloat (data ["BuddhaPercentage"]);

		this.Day = (int)(long)data ["Day"];

		this.StomachStartValue = MyType.ObjectToFloat (data ["StomacStartValue"]);
		this.HotokeStartValue = MyType.ObjectToFloat (data ["HotokeStartValue"]);
		this.FaithStartValue = MyType.ObjectToFloat (data ["FaithStartValue"]);

		this.StomachMaxValue = MyType.ObjectToFloat (data ["StomacMaxValue"]);
		this.HotokeMaxValue = MyType.ObjectToFloat (data ["HotokeMaxValue"]);
		this.FaithMaxValue = MyType.ObjectToFloat (data ["FaithMaxValue"]);

		this.StomachLostTime = MyType.ObjectToFloat (data ["StomachLostTime"]);

		this.StomacCoolTime = MyType.ObjectToFloat (data ["StomacCoolTime"]);
		this.TrainingCoolTime = MyType.ObjectToFloat (data ["ShugyoCoolTime"]);
		this.PreachingCoolTime = MyType.ObjectToFloat (data ["SeppooCoolTime"]);

		this.GainStomacValue = MyType.ObjectToFloat (data ["GainStomacValue"]);
		this.GainHotokeValue = MyType.ObjectToFloat (data ["GainHotokeValue"]);
		this.GainFaithValue = MyType.ObjectToFloat (data ["GainFaithValue"]);
	}
};

public struct BuddhistNames
{
	public List<string> ingouList;
	public List<string> dougouList;
	public List<string> igou_maleList;
	public List<string> igou_femaleList;


	public BuddhistNames(Dictionary<string, object> data)
	{
		ingouList = (data ["ingou"] as List<object>).Select (x => (string)x).ToList ();
		dougouList = (data ["dougou"] as List<object>).Select (x => (string)x).ToList ();
		igou_maleList = (data ["igou_male"] as List<object>).Select (x => (string)x).ToList ();
		igou_femaleList = (data ["igou_female"] as List<object>).Select (x => (string)x).ToList ();
	}
};