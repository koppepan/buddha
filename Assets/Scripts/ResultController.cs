using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ResultController : MonoBehaviour {

	[SerializeField]
	Text BuddhistName;
	[SerializeField]
	Image peka;

	void Awake()
	{
		SoundManager.Instance.PlayBGM (e_BgmSound.Ending);
		
		var instance = MainSystem.Instance;

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
			instance.PlayerName.Replace (" ", string.Empty);
			instance.PlayerName.Replace("\n", string.Empty);
			if (instance.PlayerName != string.Empty) {
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
