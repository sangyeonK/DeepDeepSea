﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Reflection;
using System.Linq;

public class TitleSceneManager : MonoBehaviour
{

    [AttributeUsage(AttributeTargets.Field)]
    class PopUpAttribute : Attribute { };

    [Header("Canvas UI")]
    [PopUp]
    public RectTransform mTeamView;
    [PopUp]
    public RectTransform quitPanel;
    public RectTransform startButton;
    [PopUp]
    public RectTransform playHistoryPanel;
    [PopUp]
    public RectTransform bestScorePanel;

    private List<RectTransform> popups = new List<RectTransform>();
    private void Start()
    {
        CollectPopups();

        StartCoroutine(ActiveStartButton());
        StartCoroutine(Global.Instance.TemporarySavedDataManager.SaveToOnline());
    }

    IEnumerator ActiveStartButton()
    {
        yield return new WaitForSeconds(3f);

        startButton.gameObject.SetActive(true);
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnClickTeamButton()
    {
        mTeamView.gameObject.SetActive(true);
    }

    public void OnClickPlayHistoryButton()
    {
        playHistoryPanel.gameObject.SetActive(true);
    }

    public void OnClickBestScoreButton()
    {
        bestScorePanel.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (popups.All(popup => !popup.gameObject.activeSelf))
            {
                quitPanel.gameObject.SetActive(true);
            }
        }
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    private void CollectPopups()
    {
        popups.Clear();
        FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            foreach (Attribute attr in field.GetCustomAttributes(true))
            {
                PopUpAttribute popup = attr as PopUpAttribute;
                if (popup != null)
                {
                    RectTransform obj = field.GetValue(this) as RectTransform;
                    if (obj != null)
                        popups.Add(obj);

                }
            }
        }
    }

}
