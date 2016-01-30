using UnityEngine;
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

public struct NoteData
{
	public float Radius;
	public float Speed;
	public string Message;
	public NoteData(float Radius, float Speed, string Message)
	{
		this.Radius = Radius;
		this.Speed = Speed;
		this.Message = Message;
	}
	public NoteData(Dictionary<string, object> data)
	{
		Radius = MyType.ObjectToFloat (data ["Radius"]);
		Speed = MyType.ObjectToFloat (data ["Speed"]);
		Message = (string)data ["Message"];
	}
	public object Serialize()
	{
		var data = new Dictionary<string, object> ();
		data ["Radius"] = Radius;
		data ["Speed"] = Speed;
		data ["Message"] = Message;
		return data;
	}
};

