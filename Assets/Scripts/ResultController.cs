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
				instance.PlayerName = instance.PlayerName.Replace (" ", string.Empty);
				instance.PlayerName = instance.PlayerName.Replace ("\n", string.Empty);

				if (instance.PlayerName.Length >= 2) {
					for (int i = 0; i < 2; i++) {
						var index = Random.Range (0, instance.PlayerName.Length);
						BuddhistName.text += instance.PlayerName [index];
					}
					BuddhistName.text += instance.PlayerName.Substring (0, 2);
				} else {
					BuddhistName.text += instance.PlayerName;
				}
			}
		}
		{
			var index = Random.Range (0, instance.buddhistNames.igou_maleList.Count);
			BuddhistName.text += instance.buddhistNames.igou_maleList [index];
		}

		var finishType = MainSystem.Instance.FinishState;
		player.SetTexture (finishType);
		peka.enabled = (finishType == Player.StateType.best) || (finishType == Player.StateType.die);
		SetBgSprite (MainSystem.Instance.FinishFaithValue);

		Debug.Log (finishType);
		Debug.Log (MainSystem.Instance.FinishFaithValue);
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
