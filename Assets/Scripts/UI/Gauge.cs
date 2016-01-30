using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Gauge : MonoBehaviour {

	[SerializeField]
	Image gauge;

	public void Set(float f)
	{
		gauge.fillAmount = f;
	}
}
