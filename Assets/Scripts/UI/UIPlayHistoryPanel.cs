using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayHistoryPanel : MonoBehaviour {

    public Text totalPlay;
    public Text times;
    public Text playTime;
    public Text depth;
    public Text meter;

    public Text bestDepthText;
    public Text bestStage;

    public Text averageStage;
    public Text averageMeter;
    public Text averageDepth;

    public Sprite[] backImages;
    public RawImage backImage;
    public Sprite[] mapImages;
    public RawImage mapImage;


    GameData gameData;

    // Use this for initialization
    void Start () {
        gameData = Global.Instance.LocalPlayHistoryManager.LoadGameData();

        SetDatas();
    }

    private void SetDatas()
    {
        totalPlay.text = gameData.history.Count.ToString();

        int totalPlayCount = gameData.history.Count;

        int totalPlayTime = 0;
        int totalDepth = 0;

        int bestDepth = 0;

        foreach (GameData.Record data in gameData.history)
        {
            totalPlayTime += data.playTime;
            totalDepth += data.depth;
            if (bestDepth < data.depth)
            {
                bestDepth = data.depth;
            }
        }

        TimeSpan timeSpan = TimeSpan.FromSeconds(totalPlayTime);
        string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);


        totalPlay.text = totalPlayCount.ToString() + " times";
        playTime.text = timeText;
        depth.text = totalDepth.ToString() + "M";

        int average_depth = totalPlayCount > 0 ? totalDepth / totalPlayCount : 0;
        bestDepthText.text = bestDepth.ToString() + " M";
        averageDepth.text = average_depth.ToString() + "M";

        bestStage.text = Define.DepthToName(bestDepth);
        averageStage.text = Define.DepthToName(average_depth);

        int bestLevel = Define.PELAGIC.GetLevel(bestDepth);
        int averageLevel = Define.PELAGIC.GetLevel(average_depth);

        // 플레이어 기록에 따라 이미지 변경
        if (bestLevel - 1 < backImages.Length)
        {
            backImage.texture = backImages[bestLevel - 1].texture;
        }
        if (averageLevel - 1 < mapImages.Length)
        {
            mapImage.texture = mapImages[averageLevel - 1].texture;
        }

    }
}
