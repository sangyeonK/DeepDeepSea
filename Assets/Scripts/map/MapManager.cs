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
    public GAMESTAGETYPE gamestageType;
    public float EpilagicZone=200;//표해수층 0~200m
    public  float MesopelagicZone=1000;//중심해층 200~1000m
    public float BathypelagicZone=3000;//점심해층 1000~3000m
    public float AbyssopelagicZone=6000;//심해저대 3000m~6000m
    public float HadalpelagicZone=10000;//초심해저대 6000m~
    
    public Text seameterText;
    public string zoneName;

    
    float interval = 0;
    public bool gameover;
    private Image moveIMG;
    private float waittime;
    private   float creatDistance;
    private float destroyTime;
    public Text timer;
    
    [SerializeField]
    public GameObject[] stage1Obj;
    public GameObject[] stage1Ob2;
    public GameObject[] stage1Ob3;
    public GameObject[] stage1Ob4;
    public GameObject[] stage1Ob5;
    public int rand;
   private float starttime;
    
    private void Awake(){
      player = GameObject.FindGameObjectWithTag("Player");
    }
   private  void Start()
    {
        gameover = false;
      screenObject = GameObject.FindGameObjectWithTag("ScreenObject");
      
      StartCoroutine(startMap());
    
     
    }
   private void update(){

        Vector3 pos = transform.position;
        pos.y = screenObject.transform.position.y - 5.0f;
        transform.position = pos;
       
     Debug.Log("gameover"+ " " +gameover);

   
    }
    
    public IEnumerator startMap(){
            yield return new WaitForSeconds(5.0f);
             StartCoroutine(MapCreate());
             Debug.Log("startmap");
             yield break;
    }
    
    private void Destroy( ){
      Destroy(gameObject);
    }

       public IEnumerator MapCreate()

    {
             Debug.Log("mapcreate");

          
           if(GameManager.Instance.isSpeedMode==false){
             waittime=4.0f;
             creatDistance=-8.0f;
          }
           else if(GameManager.Instance.isSpeedMode==true){
             waittime=3.0f;
             creatDistance=-5.0f;
           }
        
  
          int random=Random.Range(0,Maptype.Length);
            GameObject maptype = (GameObject)Instantiate(Maptype[random],
             screenObject.transform.position + new Vector3(0,creatDistance)
             , Quaternion.identity);  
            

            
          

              yield return new WaitForSeconds(waittime);

              StartCoroutine(MapCreate());
 
              
           }


           }
          
          
       
            
 
        
          
         
      
           








    
  
