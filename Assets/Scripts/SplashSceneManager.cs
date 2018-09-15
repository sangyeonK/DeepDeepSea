using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SplashSceneManager : MonoBehaviour
{
    public VideoPlayer movieClip;

    [Header("Canvas UI")]
    public GameObject pressAnyKeyPanel;

    bool loadingComplete = false;

    void Start()
    {
        Camera mainCarema = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        movieClip.targetCamera = mainCarema;
        movieClip.loopPointReached += (VideoPlayer source) =>
        {
            OnStartGame();
        };
        loadingComplete = false;
        StartCoroutine(LoadData());
    }


    IEnumerator LoadData()
    {
        // TODO : it is temporary loading time..
        yield return new WaitForSeconds(3.0f);

        // loading complete
        loadingComplete = true;
        pressAnyKeyPanel.SetActive(true);

    }

    private void Update()
    {
        if (loadingComplete && Input.GetMouseButtonDown(0))
        {
            OnStartGame();
        }
    }
    void OnStartGame()
    {
        if (!loadingComplete)
            return;

        SceneManager.LoadScene("TitleScene");
    }
}
