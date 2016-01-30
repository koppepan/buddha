using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public class GameController : MonoBehaviour
{
	[SerializeField]
	Player player;

	GameData gameData;
	float timeCount;

	public enum BuddhaState
	{
		human = 0,
		halfway,
		buddha
	};

	public BuddhaState BuddhaWay {
		get;
		set;
	}

	void Awake()
	{
		var textAsset = Resources.Load ("GameData") as TextAsset;
		var data = MiniJSON.Json.Deserialize (textAsset.text) as Dictionary<string, object>;

		gameData = new GameData (data);

		timeCount = 0;
		BuddhaWay = BuddhaState.human;
		PlayerSetTexture ();
	}

	void Update()
	{

		timeCount += Time.deltaTime;

		switch (BuddhaWay) {
		case BuddhaState.human:
			if (timeCount > gameData.TimeLimit.Seconds * gameData.HalfPercentage) {
				BuddhaWay = BuddhaState.halfway;
				PlayerSetTexture ();
			}
			break;
		case BuddhaState.halfway:
			if (timeCount > gameData.TimeLimit.Seconds * gameData.BuddhaPercentage) {
				BuddhaWay = BuddhaState.buddha;
				PlayerSetTexture ();
			}
			break;
		case BuddhaState.buddha:
			if (timeCount > gameData.TimeLimit.Seconds) {
				Debug.LogWarning ("Clear");
			}
			break;
		}
	}

	void PlayerSetTexture()
	{
		player.SetTexture ((int)BuddhaWay);
	}
}
