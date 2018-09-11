using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBestScoreCtrl : MonoBehaviour
{
    [Header("Canvas UI")]
    public List<Text> scoreBoardTexts;

    private void OnEnable()
    {
        TemporarySavedDataToOnline();
        RefreshBestScores();
    }

    private void TemporarySavedDataToOnline()
    {
        StartCoroutine(Global.Instance.TemporarySavedDataManager.SaveToOnline());
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

        StartCoroutine(NetworkManager.FetchBestScores((results) =>
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
