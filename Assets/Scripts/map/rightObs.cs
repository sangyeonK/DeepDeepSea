using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightObs : MonoBehaviour
{

    private float moveSpeed;
    private int x;
    private int y;
    private int z;

    private void Start()
    {
        transform.position=new Vector3( Random.Range(-5,5),Random.Range(-17,-5),z);

        x = Random.Range(-1, -3);
    }


    void Update()
    {
       

        transform.position =
           new Vector3(x * Mathf.PingPong(Time.time, 3), transform.position.y, transform.position.z);


    }




}



