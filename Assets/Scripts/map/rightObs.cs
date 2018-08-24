using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightObs : MonoBehaviour
{

    
    private int x;
    

    private void Start()
    {
        transform.position=new Vector3( Random.Range(-5,5),this.transform.position.y,this.transform.position.z);

        x = Random.Range(-1, -3);
    }


    void Update()
    {
       

        transform.position =
           new Vector3(x * Mathf.PingPong(Time.time, 3), transform.position.y, transform.position.z);


    }




}



