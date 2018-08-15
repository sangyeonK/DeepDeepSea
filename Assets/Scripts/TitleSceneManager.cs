using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour {

    public GameObject mTeamView;

    [Header("Canvas UI")]
    public GameObject quitPanel;

	static int i = 0;

    public void OnStartButton()
    {
        SceneManager.LoadScene("SampleScene");
		FileManager.Instance.Save(i, i);
		i++;
    }

    public void OnClickTeamButton()
    {
        mTeamView.SetActive(true);
		FileManager.Instance.Remove();

    }

    public void OnClickRankButton()
    {
		SceneManager.LoadScene("RankScene");
    }

    public void OnClickBackButton()
    {
        mTeamView.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(quitPanel.activeSelf == false)
            {
                quitPanel.SetActive(true);
            }

        }
    }

    public void OnQuitButton()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

}
