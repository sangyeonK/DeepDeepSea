using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pingpong : MonoBehaviour {
    
    private int x;
   private GameObject screenObject;

    void Awake(){
         screenObject = GameObject.FindGameObjectWithTag("ScreenObject");
    }


    void Start () {
       transform.position=new Vector3( Random.Range(-5,5),screenObject.transform.position.y+transform.position.y,this.transform.position.z);

        x = Random.Range(1, 2);
	}

   



    void Update()
    {
       

        transform.position =
           new Vector3(x * Mathf.PingPong(Time.time, 2), this.transform.position.y, transform.position.z);


    }

}
