using DeepDeepSeaSystem;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlayTimer : MonoBehaviour
{
    public Text gamePlayTimer;

    private void Start()
    {
        gamePlayTimer.text = Utils.MakeTimeString((int)GameManager.Instance.GamePlaySecondTimer);

        InjectEventListeners();
    }

    private void OnGamePlaySecondTick(int remainSecond)
    {
        gamePlayTimer.text = Utils.MakeTimeString(remainSecond);
    }

    private void InjectEventListeners()
    {
        GameManager.Instance.AddGamePlaySecondTickListener(OnGamePlaySecondTick);
    }
    private void OnDestroy()
    {
        //removeEventListeners
        GameManager.Instance.RemoveGamePlaySecondTickListener(OnGamePlaySecondTick);

    }
}
