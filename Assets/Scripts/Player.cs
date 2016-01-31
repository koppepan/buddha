using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public enum StateType
	{
		human = 0,
		half,
		end,
		die,
		fuhai,
		karakara,
		best
	};

	[SerializeField]
	List<Sprite> buddaTexture = new List<Sprite> ();

	[SerializeField]
	Image buddaImage;

	[SerializeField]
	AnimationCurve hotokeToStomacFactor = null;

	GameData data;
	bool hide;

	// 空腹
	private float stomac;
	private float stomacMax;
	public float Stomac {
		get { return stomac; }
		set {
			stomac = value;
			if (stomac > stomacMax) {
				stomac = stomacMax;
			}
			GUIController.Instance.SetStomachGauge (value / data.StomachMaxValue);
		}
	}

	// 即身仏
	private float hotoke;
	private float hotokeMax;
	public float Hotoke {
		get { return hotoke; }
		set {
			hotoke = value;
			if (hotoke > hotokeMax) {
				hotoke = hotokeMax;
			}
			GUIController.Instance.SetHotokeGauge (value / data.HotokeMaxValue);
		}
	}

	// 信仰
	private float faith;
	private float faithMax;
	public float Faith {
		get { return faith; }
		set { 
			faith = value;
			if (faith > faithMax) {
				faith = faithMax;
			}
		}
	}

	public StateType nowType {
		get;
		private set;
	}

	void Start()
	{
		data = MainSystem.Instance.gameData;

		nowType = StateType.human;
		SetTexture (nowType);

		SetData ();

		GUIController.Instance.SetCoolTime (data.StomacCoolTime, data.TrainingCoolTime, data.PreachingCoolTime);

		GUIController.Instance.OnButtonDownStomac = () => {
			Stomac += data.GainStomacValue;
		};
		GUIController.Instance.OnButtonDownTreaning = () => {
			Hotoke += data.GainHotokeValue;
			hide = true;
		};
		GUIController.Instance.OnButtonDownPreaching = () => {
			Faith += data.GainFaithValue;
			hide = true;
		};

		GUIController.Instance.OnCoolFinish = () => {
			hide = false;
		};
	}

	private void SetData()
	{
		this.stomacMax = data.StomachMaxValue;
		this.hotokeMax = data.HotokeMaxValue;
		this.faithMax = data.FaithMaxValue;

		this.Stomac = data.StomachStartValue;
		this.Hotoke = data.HotokeStartValue;
		this.Faith = data.FaithStartValue;
	}

	private void SetTexture(StateType index)
	{
		buddaImage.sprite = buddaTexture [(int)index];
	}

	void Update()
	{
		if (hide && buddaImage.rectTransform.localPosition.x < 800) {
			var pos = buddaImage.rectTransform.localPosition;
			buddaImage.rectTransform.localPosition = new Vector3 (pos.x + Time.deltaTime * 3000, pos.y, pos.z);
		} else if (!hide && buddaImage.rectTransform.localPosition.x > 0) {
			var pos = buddaImage.rectTransform.localPosition;
			buddaImage.rectTransform.localPosition = new Vector3 (pos.x - Time.deltaTime * 3000, pos.y, pos.z);
			if (buddaImage.rectTransform.localPosition.x < 0) {
				buddaImage.rectTransform.localPosition = new Vector3 (0, pos.y, pos.z);
			}
		}
	}

	public void TimeUpdate(float nowTime)
	{
		if (Stomac < 0) {
			SetTexture (nowType = StateType.die);
		} else {
			DecreaseUpdate ();
		}
			
		switch (nowType) {

		case StateType.human:
			if (nowTime > data.TimeLimit * data.HalfPercentage) {
				SetTexture (nowType = StateType.half);
			}
			break;

		case StateType.half:
			if (nowTime > data.TimeLimit * data.BuddhaPercentage) {
				SetTexture (nowType = StateType.end);
			}
			break;

		case StateType.end:
			if (nowTime > data.TimeLimit) {
				SetTexture (nowType = CalcType ());
			}
			break;
		}
	}

	StateType CalcType()
	{
		if (Stomac < 0) {
			return StateType.die;
		} else if (Hotoke >= 30 && Hotoke < 90) {
			return StateType.karakara;
		} else if (Hotoke >= 90) {
			return StateType.best;
		}
		return StateType.die;
	}

	void DecreaseUpdate()
	{
		Stomac -= (data.StomachMaxValue / data.StomachLostTime / 60f) * (hotokeToStomacFactor.Evaluate((Hotoke / hotokeMax)) + 1.0f);
	}

}
