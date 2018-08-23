using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public enum ItemKind
    {
        SPEED_BOOST
    }

    public ItemKind kind;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Character character = collision.GetComponent<Character>();
            character.GetItem(kind);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
