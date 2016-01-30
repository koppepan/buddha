using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public enum ResultType
{
	Die,
	PariPari,
	Best,
	NormalDead
};

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
		buddha,
		die
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

	public void Start()
	{
		player.SetData (gameData.StomachStartValue, gameData.HotokeStartValue, gameData.FaithStartValue,
			gameData.StomachMaxValue, gameData.HotokeMaxValue, gameData.FaithMaxValue);
		GUIController.Instance.SetCoolTime (gameData.StomacCoolTime, gameData.TrainingCoolTime, gameData.PreachingCoolTime);

		SetStomac(player.Stomac / gameData.StomachMaxValue);
		SetHotoke(player.Hotoke / gameData.GainHotokeValue);

		GUIController.Instance.OnButtonDownStomac = () => {
			player.Stomac += gameData.GainStomacValue;
			SetStomac(player.Stomac / gameData.StomachMaxValue);
		};
		GUIController.Instance.OnButtonDownTreaning = () => {
			player.Hotoke += gameData.GainHotokeValue;
			player.Hide(true);
			SetHotoke(player.Hotoke / gameData.HotokeMaxValue);
		};
		GUIController.Instance.OnButtonDownPreaching = () => {
			player.Faith += gameData.GainFaithValue;
			player.Hide(true);
		};

		GUIController.Instance.OnCoolTimeFinish = () => {
			player.Hide(false);
		};
	}

	void Update()
	{
		timeCount += Time.deltaTime;
		GUIController.Instance.SetTimeCount (timeCount, gameData.TimeLimit, gameData.Day);

		if (player.Stomac < 0) {
			BuddhaWay = BuddhaState.die;
		}

		switch (BuddhaWay) {
		case BuddhaState.human:
			if (timeCount > gameData.TimeLimit * gameData.HalfPercentage) {
				BuddhaWay = BuddhaState.halfway;
				PlayerSetTexture ();
			}
			break;
		case BuddhaState.halfway:
			if (timeCount > gameData.TimeLimit * gameData.BuddhaPercentage) {
				BuddhaWay = BuddhaState.buddha;
				PlayerSetTexture ();
			}
			break;
		case BuddhaState.buddha:
			if (timeCount > gameData.TimeLimit) {
				BuddhaWay = BuddhaState.die;
				Debug.LogWarning ("Clear");
			}
			break;
		case BuddhaState.die:
			Debug.Log (CalcResult ());
			SceneManager.LoadScene ("result");
			break;
		}

		if (BuddhaWay == BuddhaState.die) {
			return;
		}

		DecreaseUpdate ();
	}

	void PlayerSetTexture()
	{
		player.SetTexture ((int)BuddhaWay);
	}

	void DecreaseUpdate()
	{
		player.Stomac -= gameData.StomachMaxValue / gameData.StomachLostTime / 60f;
		SetStomac(player.Stomac / gameData.StomachMaxValue);
	}

	void SetStomac(float val)
	{
		GUIController.Instance.SetStomachGauge (val);
	}

	void SetHotoke(float val)
	{
		GUIController.Instance.SetHotokeGauge (val);
	}

	ResultType CalcResult()
	{
		if (timeCount < gameData.TimeLimit) {
			return ResultType.Die;
		}

		if (player.Hotoke >= 30 && player.Hotoke > 90) {
			return ResultType.PariPari;
		}
		if (player.Hotoke >= 90) {
			return ResultType.Best;
		}

		return ResultType.NormalDead;

	}
}
