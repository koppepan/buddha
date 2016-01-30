using UnityEngine;
using System;
using System.Collections;
using UniRx;

public class GameController : MonoBehaviour
{
	TimeSpan TimeLimit;

	void Awake()
	{
		Debug.Log ("start");
		TimeLimit = TimeSpan.FromSeconds (3650);

		Observable.Timer (TimeLimit)
			.Subscribe (_ => {
			Debug.Log ("Game Over");
		}).AddTo (this.gameObject);
	}
}
