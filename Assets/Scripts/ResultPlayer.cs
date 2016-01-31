using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResultPlayer : MonoBehaviour {

	[SerializeField]
	List<Sprite> buddaTexture = new List<Sprite> ();

	[SerializeField]
	Image buddaImage;

	public void SetTexture(Player.StateType type)
	{
		buddaImage.sprite = buddaTexture [(int)type];
	}
}
