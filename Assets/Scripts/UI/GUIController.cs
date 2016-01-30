using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GUIController : MonoBehaviour
{
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

	public Action OnCoolFinish = () => { };

	bool isInvacation = true;
	public bool IsInvocation {
		get { return isInvacation; }
		set {
			isInvacation = value;
			stomacButton.IsInvocation = value;
			treaningButton.IsInvocation = value;
			preachingButton.IsInvocation = value;
		}
	}

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
			IsInvocation = false;
			OnButtonDownStomac();
		};
		treaningButton.OnButtonDown += () => {
			IsInvocation = false;
			OnButtonDownTreaning();
		};
		preachingButton.OnButtonDown += () => {
			IsInvocation = false;
			OnButtonDownPreaching();
		};

		stomacButton.OnCoolFinish += () => {
			IsInvocation = true;
			OnCoolFinish();
		};
		treaningButton.OnCoolFinish += () => {
			IsInvocation = true;
			OnCoolFinish();
		};
		preachingButton.OnCoolFinish += () => {
			IsInvocation = true;
			OnCoolFinish();
		};

		IsInvocation = true;
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

