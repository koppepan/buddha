using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainSystem : MonoBehaviour {

	static bool existsInstance = false;

	static MainSystem instance;
	public static MainSystem Instance
	{
		get { return instance; }
	}

	public BuddhistNames buddhistNames {
		get;
		set;
	}

	public string PlayerName {
		get;
		set;
	}

	public Player.StateType FinishState {
		get;
		set;
	}

	public float FinishFaithValue {
		get;
		set;
	}

	void Awake () {
		// インスタンスが存在するなら破棄する
		if (existsInstance)
		{
			Destroy(gameObject);
			return;
		}

		instance = this;
		existsInstance = true;
		DontDestroyOnLoad(gameObject);
		{
			var textAsset = Resources.Load ("GameData") as TextAsset;
			var data = MiniJSON.Json.Deserialize (textAsset.text) as Dictionary<string, object>;

			gameData = new GameData (data);
		}
		{
			var textAsset = Resources.Load ("BuddhistName") as TextAsset;
			var data = MiniJSON.Json.Deserialize (textAsset.text) as Dictionary<string, object>;
			MainSystem.Instance.buddhistNames = new BuddhistNames (data);
		}

	}

	public GameData gameData {
		get;
		set;
	}
}
