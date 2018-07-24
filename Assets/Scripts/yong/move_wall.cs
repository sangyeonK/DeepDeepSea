using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_wall : MonoBehaviour {

    public float moveSpeed;
    public int x;
	// Update is called once per frame
	void Update () {

        transform.Translate(x, moveSpeed * Time.deltaTime, 0);
    }
}
