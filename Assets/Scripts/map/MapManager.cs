using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum GAMESTAGETYPE
{
    GameStart,
    GamePlay,
    GameEnd
}
public class MapManager : MonoBehaviour
{
    public GameObject[] l_wall;

    public GameObject[] r_wall;
    
    public GameObject[] ROBJ;
    public GameObject[] itemrandom;

    public GameObject[] rightObs;
    public GameObject[] leftObs;
    public GameObject[] Maptype;
    public GameObject screenObject;
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

   

    public void UpdateLevelTimer(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }

        timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }


    void Awake()
    {
        gamestageType = GAMESTAGETYPE.GameStart;
        //Debug.Log(gamestageType);

        screenObject = GameObject.FindGameObjectWithTag("ScreenObject");
    }





    void Start()
    {
        EpilagicZone = true;
        gameover = false;
        seameter = 0;
        
        // StartCoroutine("maprandom");
        if (gamestageType == GAMESTAGETYPE.GameStart)
        {         
            gamestageType = GAMESTAGETYPE.GamePlay;          
        }

    InstantiateMapType(Define.SCREEN_HEIGHT * -3);
    }

    public void Update()
    {
        if (gamestageType == GAMESTAGETYPE.GamePlay)
        {
            interval += Time.deltaTime;
            if (interval > 5.6f)
            {
                Vector3 rightwall_pos;
                Vector3 leftwall_pos;
                if (r_wall.Length > 0)
                {
                    rightwall_pos = screenObject.transform.position + new Vector3(4, -26);
                    Instantiate(r_wall[Random.Range(0, r_wall.Length)], rightwall_pos, Quaternion.identity);
                }
                if (l_wall.Length > 0)
                {
                    leftwall_pos = screenObject.transform.position + new Vector3(-5, -20);
                    Instantiate(l_wall[Random.Range(0, l_wall.Length)], leftwall_pos, Quaternion.identity);
                }
                interval = 0;        
            }

        }

        totalTime -= Time.deltaTime;
        UpdateLevelTimer(totalTime);

    }

    public IEnumerator maprandom(){

        // FIX ME : refactoring please...

        Debug.Log(gameover);
        while(gameover==false){
            yield return new WaitForSeconds(3.0f);
            Vector3 location;
            Vector3 location1;
            Vector3 location2;
            Vector3 location3;
            Vector3 location4;
            Vector3 location5;
            Vector3 location6;

            Vector3 euler = transform.eulerAngles;
            euler.z = Random.Range(-180.0f, 180.0f);
            euler.y= Random.Range(-180.0f, 180.0f);
            ROBJ[Random.Range(0, ROBJ.Length)].transform.eulerAngles = euler;
            ROBJ[Random.Range(0, ROBJ.Length)].transform.rotation = Random.rotation;
           
            float MyAngle = Random.Range(-180f, 180f);

            Quaternion quart = Quaternion.AngleAxis(MyAngle, Vector3.forward);

            float rightA = 60.0f;
            float leftA = -60.0f;
            Quaternion rightangle = Quaternion.AngleAxis(rightA, Vector3.forward);
            Quaternion leftangle = Quaternion.AngleAxis(leftA, Vector3.forward);





            rightObs[Random.Range(0,rightObs.Length)].transform.localScale = new Vector3(Random.Range(0.3f, 0.6f),Random.Range(0.3f, 0.6f), 0);
            leftObs[Random.Range(0, leftObs.Length)].transform.localScale =new Vector3(Random.Range(0.4f, 0.7f), Random.Range(0.5f, 0.6f), 0);


          //  Instantiate(게임오브젝트, 포지션, quart);



            location = screenObject.transform.position + new Vector3(Random.Range(1.4f, 2.4f),  -7.07f);
            Instantiate(itemrandom[Random.Range(0, itemrandom.Length)], location, quart);


            location1 =screenObject.transform.position + new Vector3(Random.Range(-4.0f, -2.0f), -7.07f);
            Instantiate(itemrandom[Random.Range(0, itemrandom.Length)], location1, quart);

            Debug.Log("distance");

            location2 = screenObject.transform.position + new Vector3(Random.Range(-6.0f, -4.0f),Random.Range(-8.0f, -2.0f) );
            Instantiate(ROBJ[Random.Range(0, ROBJ.Length)], location2, quart);

            location3 = screenObject.transform.position + new Vector3(Random.Range(3.0f, 5.0f), Random.Range(-14.0f, -7.9f));
            Instantiate(ROBJ[Random.Range(0, ROBJ.Length)], location3,quart);

            location4 = screenObject.transform.position + new Vector3(Random.Range(5.0f, 6.0f), Random.Range(-14.0f, -2.0f));
            Instantiate(leftObs[Random.Range(0, leftObs.Length)], location4, rightangle);
           
            location5= screenObject.transform.position + new Vector3(Random.Range(-8.0f, -7.0f), Random.Range(-14.0f, -2.0f));
            Instantiate(rightObs[Random.Range(0, rightObs.Length)], location5, leftangle);

            location6 = screenObject.transform.position + new Vector3(-3, -25);
            Instantiate(Maptype[Random.Range(0, Maptype.Length)], location6, Quaternion.identity);

            if(gameover==true){
                yield break;
            }


        }
    }

 public void InstantiateMapType(float posY)
    {
        Vector2 location =   new Vector2(0.0f, posY);
        GameObject mapTypeObj = Instantiate(Maptype[Random.Range(0, Maptype.Length)], location, Quaternion.identity);
        mapTypeObj.GetComponent<MapPreset>().SetMapManager(gameObject);
    }



   
}


