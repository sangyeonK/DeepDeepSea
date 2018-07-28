using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fast_move : MonoBehaviour {

    public float moveSpeed_Fast;
  
    // Update is called once per frame
    void Update()
    {

        transform.Translate(0, moveSpeed_Fast * Time.deltaTime, 0);
    }

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
