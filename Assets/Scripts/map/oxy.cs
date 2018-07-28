using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oxy : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Character character = collision.GetComponent<Character>();

            Destroy(gameObject);
        }
    }
}
