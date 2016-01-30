using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleController : MonoBehaviour
{

	public void GotoSokusin()
	{
		SceneManager.LoadScene ("main");
	}

}
