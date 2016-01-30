using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	[SerializeField]
	List<Sprite> buddaTexture = new List<Sprite> ();

	[SerializeField]
	Image buddaImage;

	// 空腹
	public float Hunger {
		get;
		set;
	}

	// 水分量
	public float Moisture {
		get;
		set;
	}

	// 即身仏
	public float Buddha {
		get;
		set;
	}

	// 信仰
	public float Faith {
		get;
		set;
	}

	void Awake()
	{
		SetTexture (0);
	}

	public void SetTexture(int index)
	{
		buddaImage.sprite = buddaTexture [index];
	}

}
