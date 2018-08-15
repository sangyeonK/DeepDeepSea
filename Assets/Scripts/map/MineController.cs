using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour {

	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            Destroy(gameObject);

        }


    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }



   
}
