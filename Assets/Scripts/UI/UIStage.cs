using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStage : MonoBehaviour {

    public Text stageText;
    public Text meterText;
    public Slider meterSlider;

    public void UpdateDepth(int playDepth)
    {
        string zoneName = Define.PELAGIC.GetName(playDepth);
        stageText.text = zoneName;
        meterText.text = string.Format("{0} {1}", null, playDepth);
    }
	
	
}