using DeepDeepSeaSystem;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlayTimer : MonoBehaviour
{
    public Text gamePlayTimer;

    private int _lastRemainTime = 0;

    void Update()
    {
        int remainTime = Mathf.FloorToInt(GameManager.Instance.GamePlaySecondTimer);
        if (remainTime != _lastRemainTime)
        {
            string timeString = Utils.MakeTimeString(remainTime);
            gamePlayTimer.text = timeString;

            _lastRemainTime = remainTime;
        }
    }
}
