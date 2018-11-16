﻿using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{

    public float health = 100.0f;
    private bool death;
    private bool reverse;

    private const float SHOCK_TIME = 2.0f;
    private const float SHOCK_POWER = 2.0f;
    private const float SHOCK_LEFT = 3.0f * -1;
    private const float SHOCK_RIGHT = 3.0f;
    private bool saveMode;
    private bool shockMode;
    private float shockedTime;
    private float verticalImpact;
    private float horizontalImpact;
    [Header("Canvas UI")]
    public UIHealthGauge uiHealthGauge;
    private const int HEALTH_NORMAL = 0;
    private const int HEALTH_SOSO = 1;
    private const int HEALTH_BAD = 2;
    private int currStatus;
    private int randNum;
    private int playDepth = 0;
    private int nextStageDepth = 0;

    private enum SOUND_EFFECT
    {
        ITEM,
        HURT,
        GAME_OVER,
        SWIM,
    }

    public AudioSource itemSound;
    public AudioSource hurtSound;
    public AudioSource gameOverSound;
    public AudioSource swimSound;

    private bool swimSoundRunning;

    [SerializeField]
    private float minX = -4.5f;
    [SerializeField]
    private float maxX = 4.5f;
    [SerializeField]
    private float healthLossRate = 2f;

    private void Start()
    {
        death = false;
        reverse = false;
        saveMode = false;
        shockMode = false;
        shockedTime = 0.0f;
        verticalImpact = SHOCK_POWER;
        horizontalImpact = 0.0f;
        playDepth = 0;
        nextStageDepth = Define.PELAGIC.GetMaxMeter(playDepth);
        swimSoundRunning = false;

        transform.position = new Vector2(0f, 0f);
        // OnStartPlay 가 호출되기 전까지는 script 및 sprite renderer 비활성화
        enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;

        InjectEventListeners();
    }

    private void OnStartGamePlay()
    {
        enabled = true;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    private void OnEndGamePlay()
    {
        StartCoroutine(DiePlayer());
    }

    private void PlaySound(SOUND_EFFECT sound)
    {
        switch (sound)
        {
            case SOUND_EFFECT.ITEM:
                itemSound.Play();
                break;
            case SOUND_EFFECT.HURT:
                hurtSound.Play();
                break;
            case SOUND_EFFECT.GAME_OVER:
                gameOverSound.Play();
                break;
            case SOUND_EFFECT.SWIM:
                swimSound.Play();
                break;
        }
    }

    private IEnumerator PlaySwimSound()
    {
        swimSoundRunning = true;
        while (GameManager.Instance.isSpeedMode)
        {
            PlaySound(SOUND_EFFECT.SWIM);
            yield return new WaitForSeconds(0.5f);
        }
        swimSoundRunning = false;
    }

    // Update is called once per frame
    private void Update()
    {

        CheckHealth();

        float horizontalMove = GameManager.Instance.playerHorizontalSpeed * Time.deltaTime;
        float verticalMove = GameManager.Instance.PlayerVerticalSpeed * Time.deltaTime * -1;

        if (!death)
        {
            ShockPlayer();
            if (!shockMode)
            {
                // if (Input.touchCount > 0 || Input.GetKey(KeyCode.Mouse0))
                if (reverse)
                {
                    //reverse Move
                    horizontalMove = horizontalMove * -1;
                }
                playDepth = Mathf.FloorToInt(gameObject.transform.position.y * -1);
                if (playDepth > nextStageDepth)
                {
                    GameManager.Instance.AdvanceStage();
                    nextStageDepth = Define.PELAGIC.GetMaxMeter(playDepth);
                }
                GameSceneManager.Instance.UpdateDepthText(playDepth);
                transform.Translate(horizontalMove, verticalMove, 0.0f);
            }
        }
        else
        {
            //죽었을 경우, 해류에 따라 좌우로만 캐릭터 이동
            transform.Translate(horizontalMove, 0.0f, 0.0f);
        }

        // 캐릭터 제한위치 벗어나지 않게 보정
        Vector3 currPosition = transform.localPosition;
        float clampedX = Mathf.Clamp(currPosition.x, minX, maxX);
        if (!Mathf.Approximately(clampedX, currPosition.x))
        {
            currPosition.x = clampedX;
            transform.localPosition = currPosition;
        }

#if UNITY_EDITOR
        #region development(testing) support feature
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpeedModeOn();
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            SpeedModeOff();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            MoveReverseOn();
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            MoveReverseOff();
        }
        #endregion
#endif
    }

    public void GetItem(Item.ItemKind itemKind, float value)
    {
        // 아이템 처리는 이 함수에서 수행한다.
        switch (itemKind)
        {
            case Item.ItemKind.SPEED_BOOST:
                GameManager.Instance.SpeedPlus();
                break;
            case Item.ItemKind.OXYGEN:
                health += value;
                break;
        }


        PlaySound(SOUND_EFFECT.ITEM);
    }


    private void ShockPlayer()
    {
        if (shockedTime > 0.0f)
        {
            saveMode = true;
            if (shockedTime > 1.2f)
            {
                shockMode = true;
                transform.Translate(horizontalImpact * Time.deltaTime, verticalImpact * Time.deltaTime, 0.0f);
            }
            else
            {
                shockMode = false;
            }

            shockedTime = Mathf.Max(shockedTime - Time.deltaTime, 0.0f);
        }
        else
        {
            saveMode = false;
            verticalImpact = SHOCK_POWER;
        }
        if (saveMode)
        {
            Animator ani = GetComponentInChildren<Animator>();
            ani.SetTrigger("transparent");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (death)
        {
            return;
        }

        Animator ani = GetComponentInChildren<Animator>();

        //obstacle
        if (!saveMode)
        {
            if (collision.tag == "MINE")
            {
                ani.SetTrigger("Damage");
                health -= 5;
                shockedTime = SHOCK_TIME;
                PlaySound(SOUND_EFFECT.HURT);
            }
            if (collision.tag == "lobster")
            {

                health += 10;
                itemSound.Play();
            }
            //랜덤박스 충돌시 
            if (collision.tag == "rand")
            {
                int minrandom = 0;
                int maxrandom = 3;
                randNum = Random.Range(minrandom, maxrandom);
                itemSound.Play();
                switch (randNum)
                {

                    case 0:
                        transform.localScale += new Vector3(0.05f, 0.05f, 0);
                        break;
                    case 1:
                        transform.localScale -= new Vector3(0.02f, 0.02f, 0);
                        break;
                    case 2:
                        GameManager.Instance.playerVerticalSpeed += 0.5f;
                        GameManager.Instance.playerHorizontalSpeed += 0.5f;

                        break;
                    case 3:
                        GameManager.Instance.playerVerticalSpeed -= 0.2f;
                        GameManager.Instance.playerHorizontalSpeed -= 0.2f;
                        break;

                }

            }
            if (collision.tag == "floating")
            {
                ani.SetTrigger("Damage");
                health -= 3;
                shockedTime = SHOCK_TIME;
                ani.SetTrigger("transparent");
                PlaySound(SOUND_EFFECT.HURT);
            }
            if (collision.tag == "leftwall")
            {
                ani.SetTrigger("playerleft");
                health -= 5;
                horizontalImpact = SHOCK_RIGHT;
                shockedTime = SHOCK_TIME;
                PlaySound(SOUND_EFFECT.HURT);
            }
            if (collision.tag == "rightwall")
            {
                ani.SetTrigger("playerright");
                health -= 5;
                horizontalImpact = SHOCK_LEFT;
                shockedTime = SHOCK_TIME;
                PlaySound(SOUND_EFFECT.HURT);
            }
        }
    }

    public void CheckHealth()
    {
        if (!death)
        {
            health -= Time.deltaTime * healthLossRate;

            uiHealthGauge.UpdateHealth(health);

            if (health <= 0)
            {
                health = 0;
                //player is died
                GameManager.Instance.EndGamePlay();
            }
        }
    }

    private IEnumerator DiePlayer()
    {
        if (GameManager.Instance.isSpeedMode)
        {
            SpeedModeOff();
        }

        death = true;

        Animator ani = GetComponentInChildren<Animator>();
        ani.SetBool("Dead", true);
        PlaySound(SOUND_EFFECT.GAME_OVER);
        yield return new WaitForSeconds(3f);

        GameManager.Instance.GameOver(playDepth);
    }


    //Right Button
    public void MoveReverseOn()
    {
        horizontalImpact = SHOCK_RIGHT;
        reverse = true;
    }

    public void MoveReverseOff()
    {
        horizontalImpact = SHOCK_LEFT;
        reverse = false;
    }

    //Left Button
    public void SpeedModeOn()
    {
        if (enabled && !death)
        {
            verticalImpact *= 2.0f;
            GameManager.Instance.isSpeedMode = true;
            Animator ani = GetComponentInChildren<Animator>();
            ani.SetBool("SpeedBoost", true);
            if (!swimSoundRunning)
            {
                StartCoroutine(PlaySwimSound());
            }
        }
    }

    public void SpeedModeOff()
    {
        if (enabled && !death)
        {
            verticalImpact = SHOCK_POWER;
            GameManager.Instance.isSpeedMode = false;
            Animator ani = GetComponentInChildren<Animator>();
            ani.SetBool("SpeedBoost", false);
        }
    }

    private void InjectEventListeners()
    {
        GameManager.Instance.AddStartGamePlayListener(OnStartGamePlay);
        GameManager.Instance.AddEndGamePlayListener(OnEndGamePlay);
    }

    private void OnDestroy()    // RemoveEventListeners
    {
        GameManager.Instance.RemoveStartGamePlayListener(OnStartGamePlay);
        GameManager.Instance.RemoveEndGamePlayListener(OnEndGamePlay);
    }
}
