using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultController : MonoBehaviour {

	[SerializeField]
	Text BuddhistName;
	[SerializeField]
	Image peka;

	void Awake()
	{
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
			var index1 = Random.Range (0, instance.PlayerName.Length);
			var index2 = Random.Range (0, instance.PlayerName.Length);
			BuddhistName.text += instance.PlayerName [index1];
			BuddhistName.text += instance.PlayerName [index2];
		}
		{
			var index = Random.Range (0, instance.buddhistNames.igou_maleList.Count);
			BuddhistName.text += instance.buddhistNames.igou_maleList [index];
		}
	}

	void Update()
	{
		peka.rectTransform.Rotate (new Vector3 (0, 0, 1));
	}
}
