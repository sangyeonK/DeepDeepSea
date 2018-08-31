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
    public GameObject player;
    public GAMESTAGETYPE gamestageType;
    public float EpilagicZone=200;//표해수층 0~200m
    public  float MesopelagicZone=1000;//중심해층 200~1000m
    public float BathypelagicZone=3000;//점심해층 1000~3000m
    public float AbyssopelagicZone=6000;//심해저대 3000m~6000m
    public float HadalpelagicZone=10000;//초심해저대 6000m~
    
    public Text seameterText;
    public string zoneName;

    private int waittime;
    float interval = 0;
    public bool gameover;
    private Image moveIMG;
   

    public Text timer;
    
    [SerializeField]
    public GameObject[] stage1Obj;
    public GameObject[] stage1Ob2;
    public GameObject[] stage1Ob3;
    public GameObject[] stage1Ob4;
    public GameObject[] stage1Ob5;
    public int rand;
   private float starttime;
    
    void Start()
    {
        gameover = false;
       screenObject = GameObject.FindGameObjectWithTag("ScreenObject");
        
      StartCoroutine(startMap());
    
     
    }
    void update(){

        
     
     
       
     Debug.Log("gameover"+ " " +gameover);

     Debug.Log("screenObject.transform.position.y"+ " " +screenObject.transform.position.y);
    }
    
    public IEnumerator startMap(){
            yield return new WaitForSeconds(5.0f);
             StartCoroutine(MapCreate());
             yield break;
    }
    
    public IEnumerator MapCreate()

    {
        
       
           Debug.Log("screenObject.transform.position.y " +""+screenObject.transform.position.y );
           
          if(   screenObject.transform.position.y >-200.0f){
            GameObject maptype = (GameObject)Instantiate(Maptype[Random.Range(0, 5)],
             screenObject.transform.position + 
            new Vector3(0, -10), Quaternion.identity);  
            
            
          }
          else if(  screenObject.transform.position.y<-200.0f &&screenObject.transform.position.y>-500.0f){
           GameObject maptype1 = (GameObject)Instantiate(Maptype[Random.Range(5, 11)],
             screenObject.transform.position + 
            new Vector3(0, -10), Quaternion.identity);  
           
          }
           
          
            else if(screenObject.transform.position.y<-500.0f &&screenObject.transform.position.y>-1000.0f ){
                  GameObject maptype4 = (GameObject)Instantiate(Maptype[Random.Range(18, 22)], 
            screenObject.transform.position + new Vector3(0, -10), Quaternion.identity);
 
            }
              
            else if(screenObject.transform.position.y<-1000 &&screenObject.transform.position.y >-3000){
            GameObject maptype5 = (GameObject)Instantiate(Maptype[Random.Range(22, Maptype.Length)], 
            screenObject.transform.position + new Vector3(0, -10), Quaternion.identity);
 
              
            }
            else if (screenObject.transform.position.y<-3000){
            GameObject maptype6 = (GameObject)Instantiate(Maptype[Random.Range(0, Maptype.Length)], 
            screenObject.transform.position + new Vector3(0, -10), Quaternion.identity);
            }
              yield return new WaitForSeconds(7.0f);

              StartCoroutine(MapCreate());
 
              
           }


           }
          
          
       
            
 
        
          
         
      
           








    
  
