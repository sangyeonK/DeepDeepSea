using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

    public void OnPauseButton(GameObject pausePanel)
    {
        pausePanel.SetActive(true);
        GameManager.Instance.SetPause(true);
    }

    public void OnResumeButton(GameObject pausePanel)
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
}
