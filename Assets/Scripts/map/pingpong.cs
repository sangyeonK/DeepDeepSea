using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pingpong : MonoBehaviour {
    private float moveSpeed;
    private int x;
  

    void Start () {
        transform.position=new Vector3( Random.Range(-5,5),Random.Range(-15,5),this.transform.position.z);

        x = Random.Range(1, 2);
	}

   



    void Update()
    {
       

        transform.position =
           new Vector3(x * Mathf.PingPong(Time.time, 2), transform.position.y, transform.position.z);


    }

}
