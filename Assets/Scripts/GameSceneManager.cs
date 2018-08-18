using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameSceneManager : MonoBehaviour {

    public static GameSceneManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private static GameSceneManager _instance;


    [Header("Canvas UI")]
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    private GameObject depthText;

    private void Awake()
    {
        _instance = this;

        GameObject[] uiObjects = GameObject.FindGameObjectsWithTag("UI");

        foreach(GameObject uiObject in uiObjects)
        {
            if(uiObject.name == "moveText")
            {
                depthText = uiObject;
            }
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        OnPauseButton();
    }

    public void OnPauseButton()
    {
        pausePanel.SetActive(true);
        GameManager.Instance.SetPause(true);
    }

    public void OnResumeButton()
    {
        pausePanel.SetActive(false);
        GameManager.Instance.SetPause(false);
    }

    public void OnRestartButton()
    {
        Debug.Log("SceneCount" + SceneManager.sceneCount);
        SceneManager.LoadScene("SampleScene");
    }

    public void OnTitleButton()
    {
		SceneManager.LoadScene("TitleScene");
    }

    public void OnGameOver(float playTime, int playDepth)
    {
        GameManager.Instance.SetPause(true);
        gameOverPanel.SetActive(true);
        gameOverPanel.GetComponent<UIGameOverPanel>().SetData((int)playTime, playDepth);
    }

    public void UpdateDepthText(int playDepth)
    {
        depthText.GetComponent<UIMoveText>().UpdateDepth(playDepth);
    }

    
}
