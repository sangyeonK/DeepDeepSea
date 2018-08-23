using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayGuidePanel : MonoBehaviour {

    enum STEP
    {
        STEP1 = 0,
        STEP2,
        STEP3,
    }

    STEP currentStep;
    public GameObject[] steps = new GameObject[(int)STEP.STEP3];

	// Use this for initialization
	void Start () {
        currentStep = STEP.STEP1;
        steps[(int)STEP.STEP1].SetActive(true);
	}
	
	public void AdvanceNextStep()
    {
        steps[(int)currentStep].SetActive(false);
        if(currentStep == STEP.STEP3)
        {
            // 마지막 스탭이면 pause 해제 후 PlayGuide 종료
            GameManager.Instance.MarkSeePlayGuide();
            GameManager.Instance.SetPause(false);
            gameObject.SetActive(false);
            return;
        }

        currentStep++;
        steps[(int)currentStep].SetActive(true);
    }
}
