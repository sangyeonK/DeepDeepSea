using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    public GameObject gameOver;
   
    public sealed class Singleton
    {
        private static Singleton instance = null;
        private static readonly object padlock = new object();

        private Singleton()
        {
        }

        public static Singleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                    return instance;
                }
            }
        }
    }

    public float moveSpeed = 2.0f;
    public float health = 100.0f;
    private const float coef = 0.2f;
    public bool death;

    public Slider playerSlider;


     void Start()
    {
        if (death == false)
        {
            DecreseEnemySlider();
            gameOver.SetActive(false);

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
            float timeSlice = (slider.value /100   );
            while (slider.value >= 0)
            {
                health = slider.value;
                slider.value -= timeSlice;
                yield return new WaitForSeconds(1);
                if (slider.value <= 0)
                    break;
            }
        }
        yield return null;
    }

    // float moveSpeedBoostTime = 0.0f;

    // Update is called once per frame
    void Update () {
        
        float horizontalMove = GameManager.Instance.playerHorizontalSpeed * Time.deltaTime;
        float verticalMove = GameManager.Instance.PlayerVerticalSpeed * Time.deltaTime * -1;
        if (Input.touchCount>0 || Input.GetKey(KeyCode.Mouse0))
        {
            //reverse Move
            horizontalMove = horizontalMove * -1;
        }

        transform.Translate(horizontalMove, verticalMove, 0.0f);
        GameOverMgr();

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
        if (collision.tag == "MINE")
        {
            Animator ani = GetComponent<Animator>();
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
            moveSpeed += 1;

        }
        if (collision.tag == "slow")
        {
            moveSpeed -= 1;

        }

        if (collision.tag == "floating")
        {
            Animator ani = GetComponent<Animator>();
            ani.SetTrigger("Damage");
            health -= 5;
            ani.SetTrigger("transparent");

        }
        if (collision.tag == "leftwall")
        {
            Animator ani = GetComponent<Animator>();
            ani.SetTrigger("playerleft");
            health -= 5;
           

        }
        if (collision.tag == "rightwall")
        {
            Animator ani = GetComponent<Animator>();
            ani.SetTrigger("playerright");
            health -= 5;


        }

    }

    public void GameOverMgr()
    {
        if (health <0)
        {
            StartCoroutine("death_time");
            gameOver.SetActive(true);
            GameManager.Instance.SetPause(true);
            Debug.Log("gameOver");
        }

    }


    public IEnumerator death_time(){


        yield return new WaitForSeconds(1.0f);

        Animator ani = GetComponent<Animator>();
        ani.SetTrigger("Dead");
        death = true;
        yield break;


    }
}
