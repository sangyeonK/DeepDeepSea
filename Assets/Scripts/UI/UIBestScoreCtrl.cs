using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBestScoreCtrl : MonoBehaviour
{
    [Header("Canvas UI")]
    public List<Text> scoreBoardTexts  ;
    private Coroutine crtFetchBestScores = null;

    private void OnEnable()
    {
        RefreshBestScores();
    }
    private void ResetBestScores()
    {
        for (int i = 0; i < scoreBoardTexts.Count; i++)
        {
            scoreBoardTexts[i].text = "-";
        }
    }

    private void RefreshBestScores()
    {
        ResetBestScores();

        if (crtFetchBestScores != null)
            StopCoroutine(crtFetchBestScores);

        crtFetchBestScores = StartCoroutine(NetworkManager.FetchBestScores((results) =>
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (scoreBoardTexts != null && scoreBoardTexts.Count > i)
                {
                    scoreBoardTexts[i].text = String.Format("{0:n0} m", results[i]);
                }
            }
            if (scoreBoardTexts.Count - results.Count > 0)
            {
                for (int i = results.Count; i < scoreBoardTexts.Count; i++)
                {
                    scoreBoardTexts[i].text = "0 m";
                }
            }

        }, null));
    }
}
