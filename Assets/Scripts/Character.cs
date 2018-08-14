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

    public Slider playerSlider;

    private float playTime = 0.0f;
    private int playDepth = 0;


    public AudioClip swimSound;
    public AudioSource Audio;

    void Start()
    {
        death = false;
        reverse = false;
        playTime = 0.0f;
        playDepth = 0;

        DecreseEnemySlider();


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
        if (!death)
        {
            
            // if (Input.touchCount > 0 || Input.GetKey(KeyCode.Mouse0))
            if(reverse)
            {
                //reverse Move
                horizontalMove = horizontalMove * -1;
                AudioSource audio2 = GetComponent<AudioSource>();
                audio2.Play();
            }

            playTime += Time.deltaTime;
            playDepth = Mathf.FloorToInt(gameObject.transform.position.y * -1);
            GameSceneManager.Instance.UpdateDepthText(playDepth);
        }
        transform.Translate(horizontalMove, verticalMove, 0.0f);
    }

    public void GetItem(Item.ItemKind itemKind)
    {
        switch (itemKind) 
        {
            case Item.ItemKind.SPEED_BOOST:
                GameManager.Instance.SpeedPlus();
                break;
        }

    }

    



   




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (death)
            return;

        Animator ani = GetComponentInChildren<Animator>();

        if (collision.tag == "MINE")
        {
            
            ani.SetTrigger("Damage");
            health -= 5;
            Debug.Log("collider mine");
        }


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

        if (collision.tag == "floating")
        {
            ani.SetTrigger("Damage");
            health -= 5;
            ani.SetTrigger("transparent");
        }
        if (collision.tag == "leftwall")
        {
            ani.SetTrigger("playerleft");
            health -= 5;
            Debug.Log("playerleft");
        }
        if (collision.tag == "rightwall")
        {
            ani.SetTrigger("playerright");
            health -= 5;
            Debug.Log("playerright");
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
        reverse = true;
    }

    public void MoveReverseOff() {
        reverse = false;
    }

    //Left Button
    public void SpeedModeOn() {
        if (!death)
        {   
            Debug.Log("SpeedModeOn");
            GameManager.Instance.isSpeedMode = true;
            Animator ani = GetComponentInChildren<Animator>();
            ani.SetBool("SpeedBoost", true);
        }
    }
    public void SpeedModeOff() {
        if (!death)
        {   
            Debug.Log("SpeedModeOff");
            GameManager.Instance.isSpeedMode = false;
            Animator ani = GetComponentInChildren<Animator>();
            ani.SetBool("SpeedBoost", false);
        }
    }
}
