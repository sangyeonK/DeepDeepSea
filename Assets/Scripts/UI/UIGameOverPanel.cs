﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Util;

public class UIGameOverPanel : MonoBehaviour {

    Text playTime;
    Text stage;

    private void Awake()
    {
        foreach(var textComponent in GetComponentsInChildren<Text>())
        {
            if(textComponent.name == "PlayTime")
            {
                playTime = textComponent;
            }
            if (textComponent.name == "Stage")
            {
                stage = textComponent;
            }

        }
    }

    public void SetData(int playTime, int playDepth)
    {
        TimeSpan ts = new TimeSpan(0, 0, playTime);
        
        this.playTime.text = ts.ToString();
		this.stage.text = Define.DepthToName(playDepth);

		FileManager.Instance.Save(playTime, playDepth);
    }

    

}



