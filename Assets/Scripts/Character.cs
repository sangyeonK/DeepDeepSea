using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {


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


	public float moveSpeed;
    public float health = 100.0f;
    private const float coef = 0.2f;
    public bool death;

    public Slider playerSlider;


     void Start()
    {
        if (death == false)
        {
            DecreseEnemySlider();
           
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


       

        // if (moveSpeedBoostTime > 0.0f)
        // {
        //     GameManager.Instance.moveSpeedBoost = true;
        //     moveSpeedBoostTime = Mathf.Max(moveSpeedBoostTime - Time.deltaTime, 0.0f);
        // }
        // else
        // {
        //     GameManager.Instance.moveSpeedBoost = false;
        // }

        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

    }





    public void GetItem(Item.ItemKind itemKind)
    {
        switch (itemKind) {
            case Item.ItemKind.SPEED_BOOST:
                // moveSpeedBoostTime = 5.0f;
                GameManager.Instance.SpeedPlus();
                break;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MINE")
        {
            Animator ani = GetComponent<Animator>();
            ani.SetTrigger("Damage");
            Debug.Log("collider mine");
        }


        if (collision.tag == "OXY")
        {
            health += 5;

                
        }




    }
}
