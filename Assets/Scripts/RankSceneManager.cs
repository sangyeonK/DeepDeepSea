using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class RankSceneManager : MonoBehaviour {

	public Text totalPlay;
    public Text times;
    public Text playTime;
    public Text depth;
    public Text meter;

    public Text bestDepthText;
    public Text bestStage;

	public Image averageImage;
    public Text averageStage;
    public Text averageMeter;
    public Text averageDepth;

   
	List<GameData> gameDatas;


	// Use this for initialization
	void Start () {

		gameDatas = FileManager.Instance.LoadGameData();

		Debug.Log(gameDatas.Count);
		foreach(GameData data in gameDatas) {
			Debug.Log(data.playTime + "/" + data.depth);
		}

		SetDatas();

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnClickBackButton()
    {
		SceneManager.LoadScene("TitleScene");
    }

	private void SetDatas()
    {
		totalPlay.text = gameDatas.Count.ToString();

		int totalPlayCount = gameDatas.Count;

        int totalPlayTime = 0;
        int totalDepth = 0;

        int bestDepth = 0;
        
        foreach (GameData data in gameDatas)
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

		bestDepthText.text = bestDepth.ToString() + " M";
		averageDepth.text = (totalDepth / totalPlayCount).ToString() + "M";

       
    }

}
