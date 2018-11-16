using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStage : MonoBehaviour {

    public Text stageText;
    public Text meterText;
    public Slider meterSlider;
    private int currMinMeter;
    private int currMaxMeter;

    void Start () {
		currMinMeter = 0;
        currMaxMeter = 200;
        meterSlider.minValue = currMinMeter;
        meterSlider.maxValue = currMaxMeter; 
        meterSlider.value = 0;
	}

    public void UpdateDepth(int playDepth)
    {
        string zoneName = Define.PELAGIC.GetName(playDepth);
        stageText.text = zoneName;
        meterText.text = string.Format("{0} {1}", null, playDepth);

        int maxMeter = Define.PELAGIC.GetMaxMeter(playDepth);
        if(currMaxMeter < maxMeter) {
            meterSlider.minValue = currMaxMeter;
            meterSlider.maxValue = maxMeter; 
        } 
        meterSlider.value = playDepth;
    }

    public void SliderPosition(float meter) {
        meterSlider.value = meter;
    }	
}