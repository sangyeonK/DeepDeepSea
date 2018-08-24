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




    public GameObject[] Maptype;
    private GameObject screenObject;

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
        InvokeRepeating("SpawnMapType", 7, 8.5f);

        if (gamestageType == GAMESTAGETYPE.GameStart)
        {
            gamestageType = GAMESTAGETYPE.GamePlay;
            //      StartCoroutine(maprandom(5.0f));
        }

        //InstantiateMapType(Define.SCREEN_HEIGHT * -3);
    }

    public void Update()
    {
        if (gamestageType == GAMESTAGETYPE.GamePlay)
        {


        }

        totalTime -= Time.deltaTime;
        UpdateLevelTimer(totalTime);

    }

    void SpawnMapType()
    {

        if (gameover == false && totalTime > 260)
        {
            GameObject maptype = (GameObject)Instantiate(Maptype[Random.Range(0, 3)], screenObject.transform.position + new Vector3(0, -25), Quaternion.identity);
        }
        else if (gameover == false && totalTime < 260)
        {
            GameObject maptype = (GameObject)Instantiate(Maptype[Random.Range(4, Maptype.Length)], screenObject.transform.position + new Vector3(0, -35), Quaternion.identity);
        }
    }


    /* 
 public IEnumerator maprandom(float waitTime){

        // FIX ME : refactoring please...

        Debug.Log(gameover);

        while(gameover==false){

            waitTime = 5.0f;
            yield return new WaitForSecondsRealtime(waitTime);
            Vector3[] location =new Vector3[4];
            
            location[0] = screenObject.transform.position + new Vector3(0, -25);

            Instantiate(Maptype[Random.Range(0, Maptype.Length)], location[0], Quaternion.identity);

            yield return new WaitForSecondsRealtime(waitTime);

            if (gameover==true){
                yield break;
            }


        }
    }

    */



    /*

     public void InstantiateMapType(float posY)
    {
        Vector2 location = new Vector2(0.0f, posY);
        GameObject mapTypeObj = Instantiate(Maptype[Random.Range(0, Maptype.Length)], location, Quaternion.identity);
        mapTypeObj.GetComponent<MapPreset>().SetMapManager(gameObject);
    }

    */




}




