using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pingpong : MonoBehaviour {
    public float moveSpeed;
    public int x;
    public int y;


    void Start () {
        x = Random.Range(1, 2);
	}

           void Update()     {                  transform.position =            new Vector3(x * Mathf.PingPong(Time.time, 2), transform.position.y, transform.position.z);       } 
}
