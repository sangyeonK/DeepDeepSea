using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// GameManager Class - Singleton Object
public class GameManager : MonoBehaviour {

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static GameManager instance = null;

    private float speedMode = 2.0f;

    private float boostedBackgroundSpeed = 0.001f;
    [HideInInspector]
    public bool isSpeedMode = false;

    [HideInInspector]
    public bool isPaused = false;

    public bool CanPaused
    {
        get;
        set;
    }

    public delegate void StartPlayHandler();
    event StartPlayHandler onStartPlay;
    /// <summary>
    /// 플레이어의 세로축 이동 속도
    /// </summary>
    public float PlayerVerticalSpeed
    {
        get
        {
            if (isSpeedMode)
            {
                return playerVerticalSpeed * speedMode;
            }
            else
            {
                return playerVerticalSpeed;
            }
        }
    }
    [SerializeField]
    public float playerVerticalSpeed = 3.0f;   // 플레이어의 세로축 이동 속도 ( private )
    public float playerHorizontalSpeed = 3.0f;  // 플레이어의 가로축 이동 속도
    public bool AlwaysSeePlayGuide = false;

    private GameData gameData;

    public float BackgroundRockTranslated
    {
        get;
        set;
    }

    private void Awake()
    {
        // 이미 해당 인스턴스 가 존재한다면 현재 오브젝트는 삭제
        if(instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        gameData = Global.Instance.LocalPlayHistoryManager.LoadGameData();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // GameScene 로딩되었을 때 처리
        InitGameData();
        SetPause(false);
        CanPaused = false;
    }
    
    void InitGameData()
    {
        BackgroundRockTranslated = 0.0f;
        isSpeedMode = false;
    }

    public void SpeedPlus() {
        playerVerticalSpeed += boostedBackgroundSpeed;
    }

    public void SetPause(bool pause)
    {
        if (pause)
        {
            this.isPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            this.isPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void StartPlay()
    {
        CanPaused = true;
        onStartPlay();
        if(!gameData.seePlayGuide || AlwaysSeePlayGuide)
        {
            GameSceneManager.Instance.StartPlayGuide();
        }
    }

    public void AddStartPlayListener(StartPlayHandler listener)
    {
        onStartPlay += listener;
    }

    public void RemoveStartPlayListener(StartPlayHandler listener)
    {
        onStartPlay -= listener;
    }

    public void GameOver(float playTime, int playDepth)
    {
        GameSceneManager sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>();
        sceneManager.OnGameOver(playTime, playDepth);
        AddPlayRecord(playTime, playDepth);
    }

    public void MarkSeePlayGuide()
    {
        gameData.seePlayGuide = true;
        Global.Instance.LocalPlayHistoryManager.SaveGameData(gameData);
    }

    private void AddPlayRecord(float playTime, int playDepth)
    {
        gameData.history.Add(new GameData.Record(Mathf.FloorToInt(playTime), playDepth));
        Global.Instance.LocalPlayHistoryManager.SaveGameData(gameData);
        Global.Instance.TemporarySavedDataManager.AddData(playDepth);
        StartCoroutine(Global.Instance.TemporarySavedDataManager.SaveToOnline());
    }

    public void AdvanceStage()
    {
        GameObject backgroundCtrl = GameObject.FindGameObjectWithTag("BackgroundCtrl");
        backgroundCtrl.GetComponent<BackgroundCtrl>().AdvanceStage();
    }

}
