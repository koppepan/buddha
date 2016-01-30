using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	[SerializeField]
	List<Sprite> buddaTexture = new List<Sprite> ();

    [SerializeField]
    AnimationCurve hotokeToStomacFactor = null;

	[SerializeField]
	Image buddaImage;

	bool hide;

	// 空腹
	private float stomac;
	private float stomacMax;
	public float Stomac {
		get { return stomac; }
		set {
			stomac = value;
			if (stomac > stomacMax) {
				stomac = stomacMax;
			}
		}
	}

	// 即身仏
	private float hotoke;
	private float hotokeMax;
	public float Hotoke {
		get { return hotoke; }
		set {
			hotoke = value;
			if (hotoke > hotokeMax) {
				hotoke = hotokeMax;
			}
		}
	}

	// 信仰
	private float faith;
	private float faithMax;
	public float Faith {
		get { return faith; }
		set { 
			faith = value;
			if (faith > faithMax) {
				faith = faithMax;
			}
		}
	}

    public float GetDecreaseStomacFactor()
    {
        return hotokeToStomacFactor.Evaluate(Hotoke / hotokeMax) + 1.0f;
    }

	void Awake()
	{
		SetTexture (0);
	}

	public void SetData(float stomac, float hotoke, float faith, float stomacMax, float hotokeMax, float faithMax)
	{
		this.stomac = stomac;
		this.hotoke = hotoke;
		this.faith = faith;

		this.stomacMax = stomacMax;
		this.hotokeMax = hotokeMax;
		this.faithMax = faithMax;
	}

	public void SetTexture(int index)
	{
		buddaImage.sprite = buddaTexture [index];
	}

	void Update()
	{
		if (hide && buddaImage.color.a > 0) {
			var col = buddaImage.color;
			buddaImage.color = new Color (col.r, col.g, col.b, col.a - Time.deltaTime * 2);
		} else if (!hide && buddaImage.color.a < 1) {
			var col = buddaImage.color;
			buddaImage.color = new Color (col.r, col.g, col.b, col.a + Time.deltaTime * 2);
		}
	}

	public void Hide(bool enable)
	{
		hide = enable;
	}
}
