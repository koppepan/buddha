using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GUIController : MonoBehaviour
{
	public Action OnCoolTimeFinish = () => { };
	private bool nowCool = false;
	public bool NowCool
	{
		get { return nowCool; }
		set{
			nowCool = value;
			if (!nowCool) {
				OnCoolTimeFinish();
			}
		}
	}

	[SerializeField]
	Text TimeCount;

	[SerializeField]
	Gauge stomachGauge;
	[SerializeField]
	Gauge hotokeGauge;

	[SerializeField]
	SkillButton stomacButton;
	[SerializeField]
	SkillButton treaningButton;
	[SerializeField]
	SkillButton preachingButton;

	public Action OnButtonDownStomac = () => { };
	public Action OnButtonDownTreaning = () => { };
	public Action OnButtonDownPreaching = () => { };

	const string CountText = "即身仏まで{0}日";

	static GUIController instance;
	public static GUIController Instance
	{
		get { return instance; }
		set { instance = value; }
	}

	void Awake()
	{
		instance = this;

		stomacButton.OnButtonDown += () => {
			OnButtonDownStomac();
		};
		treaningButton.OnButtonDown += () => {
			OnButtonDownTreaning();
		};
		preachingButton.OnButtonDown += () => {
			OnButtonDownPreaching();
		};
	}

	public void SetTimeCount(float now, float limit, int day)
	{
		TimeCount.text = string.Format (CountText, Mathf.Floor (Mathf.Lerp (day, 0, now / limit)));
	}

	public void SetStomachGauge(float f)
	{
		stomachGauge.Set (f);
	}

	public void SetHotokeGauge(float f)
	{
		hotokeGauge.Set (f);
	}

	public void SetCoolTime(float stomac, float treaning, float preaching)
	{
		stomacButton.SetCoolTime (stomac);
		treaningButton.SetCoolTime (treaning);
		preachingButton.SetCoolTime (preaching);
	}
}

