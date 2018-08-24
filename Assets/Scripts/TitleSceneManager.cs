using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour {

    [Header("Canvas UI")]
    public RectTransform mTeamView;
    public RectTransform quitPanel;
    public RectTransform startButton;
    public RectTransform playHistoryPanel;

    private void Start()
    {
        StartCoroutine(ActiveStartButton());
    }

    IEnumerator ActiveStartButton()
    {
        yield return new WaitForSeconds(3f);
        
        startButton.gameObject.SetActive(true);
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClickTeamButton()
    {
        mTeamView.gameObject.SetActive(true);
    }

    public void OnClickPlayHistoryButton()
    {
        playHistoryPanel.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(quitPanel.gameObject.activeSelf == false)
            {
                quitPanel.gameObject.SetActive(true);
            }

        }
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

}
