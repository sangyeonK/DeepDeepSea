using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankSceneManager : MonoBehaviour {

	List<GameData> gameDatas;
	// Use this for initialization
	void Start () {

		gameDatas = FileManager.Instance.LoadGameData();

		Debug.Log(gameDatas.Count);
		foreach(GameData data in gameDatas) {
			Debug.Log(data.playTime + "/" + data.depth);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnClickBackButton()
    {
		SceneManager.LoadScene("TitleScene");
    }
}
