using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (moveSpeedBoost)
                return boostedBackgroundSpeed;
            else
                return backgroundSpeed;
        }
    }
    [SerializeField]
    private float backgroundSpeed = 0.05f;
    [SerializeField]
    private float boostedBackgroundSpeed = 0.075f;
    [HideInInspector]
    public bool moveSpeedBoost = false;

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
    }
}
