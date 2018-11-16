using DeepDeepSeaSystem;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverPanel : MonoBehaviour
{
    public Text playTime;
    public Text playDepth;

    public void SetData(int playTime, int playDepth)
    {
        this.playTime.text = Utils.MakeTimeString(playTime);
        this.playDepth.text = Define.PELAGIC.GetName(playDepth);
    }
}



