using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour {

    public GameObject mTeamView;

    public void OnStartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClickTeamButton()
    {
        mTeamView.SetActive(true);
    }

    public void OnClickRankButton()
    {

    }

    public void OnClickBackButton()
    {
        mTeamView.SetActive(false);
    }
}
