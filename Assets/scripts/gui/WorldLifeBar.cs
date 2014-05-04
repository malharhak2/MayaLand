using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class WorldLifeBar : MonoBehaviour {

	private Unit targetUnit;
	private UILabel label;
	private UISlider slider;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (targetUnit != null) {
			string txt = targetUnit.life  + "/" + targetUnit.maxLife;
			float lifePart = targetUnit.life / targetUnit.maxLife;
			label.text = txt;
			slider.value = lifePart;
		}
	}
	public void SetTarget (Transform target, Unit unitScript) {
		UIFollowTarget followScript = GetComponent<UIFollowTarget>();
		followScript.target = target;
		targetUnit = unitScript;
		Transform sliderT = transform.FindChild("lifebar");
		slider = sliderT.GetComponent<UISlider>();
		Transform labelT = sliderT.FindChild ("text");
		label = labelT.GetComponent<UILabel>();
	}
}
