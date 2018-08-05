using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_wall : MonoBehaviour {

    public float moveSpeed;
    public int x;
	// Update is called once per frame
	void Update () {

        moveSpeed = Random.Range(0.0f, 3.0f);
        transform.Translate(x, moveSpeed * Time.deltaTime, 0);
    }

   
}
