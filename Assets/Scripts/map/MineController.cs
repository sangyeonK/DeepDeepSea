using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour {

	public GameObject explosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            Destroy(gameObject);
            Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        }


    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }



   
}
