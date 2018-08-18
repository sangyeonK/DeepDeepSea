using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    public static Character Instance
    {
        get
        {
            return instance;
        }
    }
    private static Character instance = null;

    
    public float health = 100.0f;
    private const float coef = 0.2f;
    public bool death;
    public bool reverse;

    private const float SHOCK_TIME = 2.0f;
    private const float SHOCK_POWER = 2.0f;
    private const float SHOCK_LEFT = 3.0f * -1;
    private const float SHOCK_RIGHT = 3.0f;

    bool saveMode;
    bool shockMode;
    float shockedTime;
    float verticalImpact;
    float horizontalImpact;

    public Slider playerSlider;

    private float playTime = 0.0f;
    private int playDepth = 0;


    public AudioSource Audio;
    public AudioClip bgm;
    public AudioClip firstwater;
    public AudioClip gamefinal;
    public AudioClip hurt;
    public AudioClip item;
    public AudioClip stop;
    public AudioClip swim;


    void Start()
    {
        death = false;
        reverse = false;
        saveMode = false;
        shockMode = false;
        shockedTime = 0.0f;
        verticalImpact = SHOCK_POWER  * Time.deltaTime ;
        horizontalImpact = 0.0f  * Time.deltaTime ;
        playTime = 0.0f;
        playDepth = 0;

        DecreseEnemySlider();


    }


    public void SoundManager()
    {
        if(!death){
            this.Audio = this.gameObject.AddComponent<AudioSource>();
            Audio.clip = bgm;
            this.Audio.PlayOneShot(bgm);

        }
        else{
            this.Audio.Stop();
        }



    } 


    public void DecreseEnemySlider()
    {
        StartCoroutine(DecreseSlider(playerSlider));
    }

    IEnumerator DecreseSlider(Slider slider)
    {
        if (slider != null)
        {
            slider.value = health > 0 ? health : 0;
        }
        yield return new WaitForSeconds(1);

        if(!death)
            StartCoroutine(DecreseSlider(playerSlider));
    }

    // float moveSpeedBoostTime = 0.0f;

    // Update is called once per frame
    void Update () {

        CheckHealth();

        float horizontalMove = GameManager.Instance.playerHorizontalSpeed * Time.deltaTime;
        float verticalMove = GameManager.Instance.PlayerVerticalSpeed * Time.deltaTime * -1;

      
        ShockPlayer();
        if(!shockMode) {
            if (!death) {
        
        // if (Input.touchCount > 0 || Input.GetKey(KeyCode.Mouse0))
                if(reverse)
                {
                    //reverse Move
                    horizontalMove = horizontalMove * -1;
                    AudioSource audio2 = GetComponent<AudioSource>();
                    audio2.Play();
                }
                playDepth = Mathf.FloorToInt(gameObject.transform.position.y * -1);
                GameSceneManager.Instance.UpdateDepthText(playDepth);
            }
            transform.Translate(horizontalMove, verticalMove, 0.0f);
        }

        if(!death)
        {
            // 플레이어 사망상태가 아니면 플레이시간은 계속 증가
            playTime += Time.deltaTime;
        }
        
    
    }

    public void GetItem(Item.ItemKind itemKind)
    {
        switch (itemKind) 
        {
            case Item.ItemKind.SPEED_BOOST:
                Debug.Log("@###@#@");
                GameManager.Instance.SpeedPlus();
                break;
        }
    }


    private void ShockPlayer() {
        if(shockedTime > 0.0f) {
            saveMode = true;
            if(shockedTime > 1.2f){
                shockMode = true;
                transform.Translate(horizontalImpact, verticalImpact, 0.0f);
            } else {
                shockMode = false;
            }
            
            shockedTime = Mathf.Max(shockedTime - Time.deltaTime, 0.0f);
        } else  {
            saveMode = false;
            verticalImpact = SHOCK_POWER  * Time.deltaTime ;
        }
        if (saveMode) {
            Animator ani = GetComponentInChildren<Animator>();
            ani.SetTrigger("transparent");
        }
    }

    



   




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (death)
            return;

        Animator ani = GetComponentInChildren<Animator>();

        //item
        if (collision.tag == "OXY")
        {
            health += 5;
 
        }
        if (collision.tag == "fast")
        {
            GameManager.Instance.playerHorizontalSpeed += 0.01f;
            GameManager.Instance.playerVerticalSpeed += 0.01f;

        }
        if (collision.tag == "slow")
        {
            GameManager.Instance.playerHorizontalSpeed -= 0.01f;
            GameManager.Instance.playerVerticalSpeed -= 0.01f;
        }
        if (collision.tag == "big")
        {
            transform.localScale += new Vector3(0.1F, 0.1F, 0);
            health -= 5;
        }
        if (collision.tag == "small")
        {
            transform.localScale += new Vector3(-0.1F, -0.1F, 0);
            health += 5;
        }


        //obstacle
        if( !saveMode ) {
            if (collision.tag == "MINE")
            {
                ani.SetTrigger("Damage");
                health -= 5;
                shockedTime = SHOCK_TIME;
                Debug.Log("collider mine");
            }
            if (collision.tag == "floating")
            {
                ani.SetTrigger("Damage");
                health -= 5;
                shockedTime = SHOCK_TIME;
                ani.SetTrigger("transparent");
            }
            if (collision.tag == "leftwall")
            {
                ani.SetTrigger("playerleft");
                health -= 5;
                horizontalImpact = SHOCK_RIGHT * Time.deltaTime ;
                shockedTime = SHOCK_TIME;
                Debug.Log("playerleft");
            }
            if (collision.tag == "rightwall")
            {
                ani.SetTrigger("playerright");
                health -= 5;
                horizontalImpact = SHOCK_LEFT * Time.deltaTime ;
                shockedTime = SHOCK_TIME;
                Debug.Log("playerright");
            }
        }
    }

    public void CheckHealth()
    {
        if (!death) {
            health -= Time.deltaTime;

            if (health <= 0)
            {
                health = 0;
                //player is died
                StartCoroutine(DiePlayer());
            }
        }
    }

    IEnumerator DiePlayer()
    {
        death = true;

        Animator ani = GetComponentInChildren<Animator>();
        ani.SetBool("Dead", true);
        yield return new WaitForSeconds(3f);

        GameManager.Instance.GameOver(playTime, playDepth);
    }


    //Right Button
    public void MoveReverseOn() {
        horizontalImpact = SHOCK_RIGHT * Time.deltaTime ;
        reverse = true;
    }

    public void MoveReverseOff() {
        horizontalImpact = SHOCK_LEFT * Time.deltaTime ;
        reverse = false;
    }

    //Left Button
    public void SpeedModeOn() {
        if (!death)
        {   
            Debug.Log("SpeedModeOn");
            verticalImpact *= 2.0f; 
            GameManager.Instance.isSpeedMode = true;
            Animator ani = GetComponentInChildren<Animator>();
            ani.SetBool("SpeedBoost", true);
        }
    }
    public void SpeedModeOff() {
        if (!death)
        {   
            verticalImpact = SHOCK_POWER  * Time.deltaTime ;
            Debug.Log("SpeedModeOff");
            GameManager.Instance.isSpeedMode = false;
            Animator ani = GetComponentInChildren<Animator>();
            ani.SetBool("SpeedBoost", false);
        }
    }
}
