using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Util;

public class UIGameOverPanel : MonoBehaviour {

    public Text playTime;
    public Text playDepth;

    public void SetData(int playTime, int playDepth)
    {
        TimeSpan ts = new TimeSpan(0, 0, playTime);
        
        this.playTime.text = ts.ToString();
        this.playDepth.text = playDepth.ToString();

        FileManager.Instance.Save(playTime, playDepth);
    }

    

}



