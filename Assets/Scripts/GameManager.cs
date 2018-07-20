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

    /// <summary>
    /// 배경 이동속도
    /// </summary>
    public float BackgroundSpeed
    {
        get
        {
            if (isSpeedMode){
                Debug.Log ("SPEEDMODE *" + speedMode +  ">>>>" +(backgroundSpeed * speedMode));
                return backgroundSpeed * speedMode;
            }
            else {
                Debug.Log ("NONE" +backgroundSpeed );
                return backgroundSpeed;
            }
                
        }
    }
    [SerializeField]
    private float backgroundSpeed = 3f;

    private float speedMode = 2.0f;

    private float boostedBackgroundSpeed = 0.001f;
    [HideInInspector]
    public bool isSpeedMode = false;

    // public bool moveSpeedBoost = false;

    /// <summary>
    /// Game Manager 단에서 제어하는 Pause UI Panel
    /// </summary>
    private GameObject pausePanel;
    [HideInInspector]
    public bool isPaused = false;

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
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetPause(false);
    }

    public void SpeedPlus() {
        backgroundSpeed += boostedBackgroundSpeed;
        Debug.Log ("GET ITEM" +backgroundSpeed );
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
}
