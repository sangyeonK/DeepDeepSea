using UnityEngine;
using UnityEngine.SceneManagement;

// GameManager Class - Singleton Object
public class GameManager : MonoBehaviour
{
    #region static members

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static GameManager instance = null;

    #endregion

    #region public members

    [HideInInspector]
    public bool isSpeedMode = false;
    [HideInInspector]
    public bool isPaused = false;

    public int timeoutSecond = 300;
    public float playerVerticalSpeed = 3.0f;   // 플레이어의 세로축 이동 속도
    public float playerHorizontalSpeed = 3.0f;  // 플레이어의 가로축 이동 속도
    public bool alwaysSeePlayGuide = false;

    #endregion

    #region attribute members

    public bool CanPaused
    {
        get;
        set;
    }
    /// <summary>
    /// 플레이어의 세로축 이동 속도
    /// </summary>
    public float PlayerVerticalSpeed
    {
        get
        {
            if (isSpeedMode)
            {
                return playerVerticalSpeed * _speedMode;
            }
            else
            {
                return playerVerticalSpeed;
            }
        }
    }
    public float BackgroundRockTranslated
    {
        get;
        set;
    }
    public float GamePlaySecondTimer
    {
        get
        {
            return _gamePlayTimer;
        }
    }

    #endregion

    #region private members

    private float _speedMode = 1.3f;
    private float _boostedBackgroundSpeed = 0.001f;
    private GAMEPLAY_STATE _gamePlayState;
    private float _gamePlayTimer;
    private int _lastRemainSecond;
    private GameData _gameData;

    #endregion

    #region delegate & event members

    public delegate void StartGamePlayHandler();
    event StartGamePlayHandler OnStartGamePlay;

    public delegate void EndGamePlayHandler();
    event EndGamePlayHandler OnEndGamePlay;

    public delegate void GamePlaySecondTickHandler(int remainSecond);
    event GamePlaySecondTickHandler OnGamePlaySecondTick;

    #endregion

    #region enums
    enum GAMEPLAY_STATE
    {
        NONE,
        PLAYING,
        END,
    }
    #endregion

    private void Awake()
    {
        // 이미 해당 인스턴스 가 존재한다면 현재 오브젝트는 삭제
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        _gameData = Global.Instance.LocalPlayHistoryManager.LoadGameData();
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
        _gamePlayState = GAMEPLAY_STATE.NONE;
        _gamePlayTimer = (float)timeoutSecond;
        _lastRemainSecond = Mathf.FloorToInt(_gamePlayTimer);
    }

    public void SpeedPlus()
    {
        playerVerticalSpeed += _boostedBackgroundSpeed;
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

    public void StartGamePlay()
    {
        CanPaused = true;
        _gamePlayState = GAMEPLAY_STATE.PLAYING;
        OnStartGamePlay();
        if (!_gameData.seePlayGuide || alwaysSeePlayGuide)
        {
            GameSceneManager.Instance.StartPlayGuide();
        }
    }

    public void EndGamePlay()
    {
        CanPaused = false;
        _gamePlayState = GAMEPLAY_STATE.END;
        OnEndGamePlay();
    }

    public void AddStartGamePlayListener(StartGamePlayHandler listener)
    {
        OnStartGamePlay += listener;
    }

    public void RemoveStartGamePlayListener(StartGamePlayHandler listener)
    {
        OnStartGamePlay -= listener;
    }

    public void AddEndGamePlayListener(EndGamePlayHandler listener)
    {
        OnEndGamePlay += listener;
    }

    public void RemoveEndGamePlayListener(EndGamePlayHandler listener)
    {
        OnEndGamePlay -= listener;
    }

    public void AddGamePlaySecondTickListener(GamePlaySecondTickHandler listener)
    {
        OnGamePlaySecondTick += listener;
    }

    public void RemoveGamePlaySecondTickListener(GamePlaySecondTickHandler listener)
    {
        OnGamePlaySecondTick -= listener;
    }

    public void GameOver(int playDepth)
    {
        float playTime = timeoutSecond - GamePlaySecondTimer;
        GameSceneManager sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>();
        sceneManager.OnGameOver(playTime, playDepth);
        AddPlayRecord(playTime, playDepth);
    }

    public void MarkSeePlayGuide()
    {
        _gameData.seePlayGuide = true;
        Global.Instance.LocalPlayHistoryManager.SaveGameData(_gameData);
    }

    private void AddPlayRecord(float playTime, int playDepth)
    {
        _gameData.history.Add(new GameData.Record(Mathf.FloorToInt(playTime), playDepth));
        Global.Instance.LocalPlayHistoryManager.SaveGameData(_gameData);
        Global.Instance.TemporarySavedDataManager.AddData(playDepth);
        StartCoroutine(Global.Instance.TemporarySavedDataManager.SaveToOnline());
    }

    public void AdvanceStage()
    {
        GameObject backgroundCtrl = GameObject.FindGameObjectWithTag("BackgroundCtrl");
        backgroundCtrl.GetComponent<BackgroundCtrl>().AdvanceStage();
    }

    private void Update()
    {
        if (_gamePlayState == GAMEPLAY_STATE.PLAYING)
        {
            _gamePlayTimer -= Time.deltaTime;
            _gamePlayTimer = _gamePlayTimer < 0 ? 0 : _gamePlayTimer;

            if (_gamePlayTimer == 0)
            {
                EndGamePlay();
            }

            int remainSecond = Mathf.FloorToInt(_gamePlayTimer);
            if (_lastRemainSecond != remainSecond)
            {
                OnGamePlaySecondTick(remainSecond);
                _lastRemainSecond = remainSecond;
            }
        }
    }

}
