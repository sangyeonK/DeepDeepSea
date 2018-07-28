using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_zone : MonoBehaviour {

	
    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
        Debug.Log("OnCollisionEnter2D");
    }







}
