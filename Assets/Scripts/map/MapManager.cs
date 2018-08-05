﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public enum GAMESTAGETYPE
{
    GameStart,
    GamePlay,
    GameEnd
}
public class MapManager : MonoBehaviour
{
    public GameObject[] mine = null;
    public GameObject[] ROBJ = null;
    public GameObject[] itemrandom = null;
    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public Slider healthSlider;


    private int damagespeed = 1;
    public GAMESTAGETYPE gamestageType;
    public bool EpilagicZone;//표해수층
    public bool MesopelagicZone;//중심해층
    public bool BathypelagicZone;//점심해층
    public bool AbyssopelagicZone;//심해저대
    public bool HadalpelagicZone;//초심해저대
    public int seameter;
    public Text seameterText;
    public string zoneName;


    private int waittime;
    float interval = 0;
    public bool gameover;
    private Image moveIMG;
   
    float totalTime = 300f; //2 minutes

    public Text timer;

    private GameObject screenObject;

    public void UpdateLevelTimer(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);

        string formatedSeconds = seconds.ToString();

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }

        timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }


    void Awake()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;

        gamestageType = GAMESTAGETYPE.GameStart;
        //Debug.Log(gamestageType);

        screenObject = GameObject.FindGameObjectWithTag("ScreenObject");
    }





    void Start()
    {
        EpilagicZone = true;
        gameover = false;
        seameter = 0;
        StartCoroutine("seameter_repeat");
        if (gamestageType == GAMESTAGETYPE.GameStart)
        {         
            gamestageType = GAMESTAGETYPE.GamePlay;          
        }
    }

    public IEnumerator seameter_repeat()
    {
      
       
        while (gameover == false)
        {
             
            yield return new WaitForSeconds(1.0f);
           
        
            switch (seameter)
            {
                case 0:zoneName = "표해수층";
                    EpilagicZone = true;
                    seameterText.text = zoneName + " " + seameter + "M";
                    Debug.Log("표해수층"); 

                    break;
                case 200:
                    MesopelagicZone = true;
                    zoneName = "중심해층";
                    Debug.Log("중심해층");

                    break;
                case 1000:
                  
                    BathypelagicZone = true;
                    zoneName = "점심해수층";
                    Debug.Log("점심해수층");

                    break;
                case 3000:
                   
                    AbyssopelagicZone = true;
                    zoneName = "심해저대";
                    Debug.Log("심해저대");
                    break;

                case 6000:
                    HadalpelagicZone = true;
                    zoneName = "초심해저대";
                    Debug.Log("초심해저대");
                    break;
                    
                   


                
            }



            seameter++;
           
           
            seameterText.text = zoneName + " "+   seameter   + "M";

        }    
        if (gameover == true)
        {

            yield break;


        }
    }
    void Update()
    {
        if (gamestageType == GAMESTAGETYPE.GamePlay)
        {
            interval += Time.deltaTime;
            if (interval > 5.6f)
            {
                Vector3 location;
                
                location = screenObject.transform.position + new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-10.0f, -4.0f));
                Instantiate(mine[Random.Range(0, mine.Length)], location, Quaternion.identity);
               
                location = screenObject.transform.position + new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-10.0f, -4.0f));
                Instantiate(ROBJ[Random.Range(0, ROBJ.Length)], location, Quaternion.identity);

                location = screenObject.transform.position + new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-10.0f, -4.0f));
                Instantiate(itemrandom[Random.Range(0, itemrandom.Length)], location, Quaternion.identity);

                interval = 0;
            }
        }

        totalTime -= Time.deltaTime;
        UpdateLevelTimer(totalTime);


       



    }

}
