using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightObs : MonoBehaviour
{

    private GameObject screenObject;
    private int x;

    void Awake(){
        screenObject = GameObject.FindGameObjectWithTag("ScreenObject");
    }

    void Start()
    {
        x = Random.Range(-1, -3);
       
        transform.position = new Vector3(Random.Range(-5,5), screenObject.transform.position.y + transform.position.y, transform.position.z);
    }

    void Update()
    {
        transform.position = new Vector3(x * Mathf.PingPong(Time.time, 3),this.transform.position.y, this.transform.position.z);
    }

}



