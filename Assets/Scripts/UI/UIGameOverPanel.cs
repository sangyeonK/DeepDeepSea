using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverPanel : MonoBehaviour {

    Text playTime;
    Text depth;

    private void Awake()
    {
        foreach(var textComponent in GetComponentsInChildren<Text>())
        {
            if(textComponent.name == "PlayTime")
            {
                playTime = textComponent;
            }
            if (textComponent.name == "Depth")
            {
                depth = textComponent;
            }

        }
    }

    public void SetData(int playTime, int playDepth)
    {
        TimeSpan ts = new TimeSpan(0, 0, playTime);

        this.playTime.text = ts.ToString();
        this.depth.text = playDepth.ToString() + "M";

		FileManager.Instance.Save(playTime, playDepth);
    }

    

}



