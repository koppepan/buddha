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

	public enum GameState
	{
		start,
		game,
		end
	};

	public GameState State {
		get;
		set;
	}
	public string PlayerName {
		get;
		set;
	}

	float time = 0;

	void Awake()
	{
		Observable.Interval (TimeSpan.FromSeconds (8)).Subscribe (_ => {
			SoundManager.Instance.PlaySE(e_SeSound.Goon);
		}).AddTo(this.gameObject);
		SoundManager.Instance.PlayBGM (e_BgmSound.Mokugyo);

		timeCount = 0;
		State = GameState.start;

		gameData = MainSystem.Instance.gameData;
	}

	void Start()
	{
		GUIController.Instance.SetTimeCount (0, gameData.TimeLimit, gameData.Day);
		GUIController.Instance.IsInvocation = false;
	}

	void OnDestroy()
	{
		SoundManager.Instance.StopBGM ();
	}

	void Update()
	{
		

		switch (State) {
		case GameState.start:
			time += Time.deltaTime;
			if (time > 1) {
				State++;
				GUIController.Instance.IsInvocation = true;
			}
			break;
		case GameState.game:
			timeCount += Time.deltaTime;
			GUIController.Instance.SetTimeCount (timeCount, gameData.TimeLimit, gameData.Day);

			player.TimeUpdate (timeCount);

			if (player.nowType > Player.StateType.end) {
				State++;
				GUIController.Instance.IsInvocation = false;
			}
			break;
		case GameState.end:
			time -= Time.deltaTime;
			if (time < 0) {
				SceneManager.LoadScene ("result");

			}
			break;

		}
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
