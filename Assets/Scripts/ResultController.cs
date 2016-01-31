using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ResultController : MonoBehaviour {

	[SerializeField]
	Text BuddhistName;
	[SerializeField]
	Image peka;

	[SerializeField]
	Image bgImage;
	[SerializeField]
	Sprite[] sprits;

	[SerializeField]
	ResultPlayer player;

	void Awake()
	{
		SoundManager.Instance.PlayBGM (e_BgmSound.Ending);
		
		var instance = MainSystem.Instance;
		if (instance == null)
			return;

		BuddhistName.text = string.Empty;
		{
			var index = Random.Range (0, instance.buddhistNames.ingouList.Count);
			BuddhistName.text += instance.buddhistNames.ingouList [index];
		}
		{
			var index = Random.Range (0, instance.buddhistNames.dougouList.Count);
			BuddhistName.text += instance.buddhistNames.dougouList [index];
		}
		{
			if (!string.IsNullOrEmpty(instance.PlayerName)) {
				instance.PlayerName.Replace (" ", string.Empty);
				instance.PlayerName.Replace ("\n", string.Empty);

				var index1 = Random.Range (0, instance.PlayerName.Length);
				BuddhistName.text += instance.PlayerName [index1];
				if (instance.PlayerName.Length > 2) {
					var index2 = Random.Range (0, instance.PlayerName.Length);
					BuddhistName.text += instance.PlayerName [index2];
				}
			}
		}
		{
			var index = Random.Range (0, instance.buddhistNames.igou_maleList.Count);
			BuddhistName.text += instance.buddhistNames.igou_maleList [index];
		}

		var finishType = MainSystem.Instance.FinishState;
		player.SetTexture (finishType);
		peka.enabled = finishType == Player.StateType.best;
		SetBgSprite (MainSystem.Instance.FinishFaithValue);
	}

	void SetBgSprite(float faith)
	{
		int index = 0;
		if (faith > 30 && faith < 70) {
			index = 1;
		} else if (faith >= 70) {
			index = 2;
		}

		bgImage.sprite = sprits [index];
	}

	void OnDestroy()
	{
		SoundManager.Instance.StopBGM ();
	}

	void Update()
	{
		peka.rectTransform.Rotate (new Vector3 (0, 0, 1));

		if (Input.anyKeyDown) {
			SceneManager.LoadScene ("title");
		}
	}
}
