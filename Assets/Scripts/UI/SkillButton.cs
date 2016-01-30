using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UniRx;

public class SkillButton : MonoBehaviour {

	[SerializeField]
	Image coolImage;

	float coolTime;

	float nowCoolTime;

	public Action OnButtonDown = () => { };

	void Awake()
	{
		coolImage.enabled = false;
	}

	public void SetCoolTime(float coolTime)
	{
		this.coolTime = coolTime;
	}

	public void OnButton()
	{
		if (GUIController.Instance.NowCool) {
			return;
		}
		if (coolImage.enabled) {
			return;
		}

		OnButtonDown ();
		coolImage.enabled = true;
		nowCoolTime = 0;

		GUIController.Instance.NowCool = true;

		SoundManager.Instance.PlaySE (e_SeSound.Chin);
	}

	void Update()
	{
		if (!coolImage.enabled) {
			return;
		}

		nowCoolTime += Time.deltaTime;

		coolImage.fillAmount = 1 - (nowCoolTime / coolTime);

		if(nowCoolTime >= coolTime)
		{
			nowCoolTime = 0;
			coolImage.fillAmount = 1;
			coolImage.enabled = false;
			GUIController.Instance.NowCool = false;
		}
	}
}
