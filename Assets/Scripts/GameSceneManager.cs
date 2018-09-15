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
    public GameObject playguidePanel;

    // private GameObject depthText;
    // private GameObject stageText;
    // private Slider depthSlider;
    public GameObject StageUI;

    private void Awake()
    {
        _instance = this;

        // GameObject[] uiObjects = GameObject.FindGameObjectsWithTag("UI");

        // foreach(GameObject uiObject in uiObjects)
        // {
        //     if( uiObject.name == "StageUI") {
        //         StageUI = uiObject;
        //     }

        //     if(uiObject.name == "moveText")
        //     {
        //         depthText = uiObject;
        //     } 
        //     if (uiObject.name == "stageText") {
        //         stageText = uiObject;
        //     }
        //     if (uiObject.name == "Meter Slider") {
        //         depthSlider = uiObject;
        //     }
        // }
    }

#if !UNITY_EDITOR
    private void OnApplicationFocus(bool focus)
    {
        OnPauseButton();
    }
#endif

    public void OnPauseButton()
    {
        if (!GameManager.Instance.isPaused && GameManager.Instance.CanPaused)
        {
            pausePanel.SetActive(true);
            GameManager.Instance.SetPause(true);
        }
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
        StageUI.GetComponent<UIStage>().UpdateDepth(playDepth);
    }

    public void StartPlayGuide()
    {
        GameManager.Instance.SetPause(true);
        playguidePanel.SetActive(true);
    }

    
}
