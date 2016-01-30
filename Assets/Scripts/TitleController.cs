using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleController : MonoBehaviour
{
	[SerializeField]
	Text text;

	void Start()
	{
		SoundManager.Instance.PlayBGM (e_BgmSound.Title);
	}

	void OnDestroy()
	{
		SoundManager.Instance.StopBGM ();
		SoundManager.Instance.PlaySE (e_SeSound.Goon);
	}

	public void GotoSokusin()
	{
		SceneManager.LoadScene ("main");
	}

	void Update()
	{
		text.color = new Color (0, 0, 0, 1 - Mathf.PingPong (Time.time, 0.5f));
	}

	public void SetName(string name)
	{
		MainSystem.Instance.PlayerName = name;
	}

}
