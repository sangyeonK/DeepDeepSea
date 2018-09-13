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
    private GameObject player;

     public bool gameover;
    private float waittime;
    private   int creatDistance;

    public int rand;
    float time=0;
    public int count=0;
     void Awake(){
      player = GameObject.FindGameObjectWithTag("Player");
    }
     void Start()
    {
        gameover = false;
        
    }
    void Update(){
      time+=Time.deltaTime;
      if(time>=2){
        count++;
        creatDistance=-28;
        int random=Random.Range(0,Maptype.Length);
            GameObject maptype = (GameObject)Instantiate(Maptype[random],
             new Vector3(0,count* creatDistance)
             , Quaternion.identity);  
            
     Debug.Log("count"+ count);
        time=0;
      }
    }
   
      }
          
          
       
            
 
        
          
         
      
           








    
  
