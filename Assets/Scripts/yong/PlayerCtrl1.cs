using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl1 : MonoBehaviour {
    public float moveSpeed = 2.0f;
   

    private void Update()
    {
        transform.Translate(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime,
            moveSpeed*Input.GetAxis("Vertical") * Time.deltaTime,
            0f);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MINE")
        {
            Destroy(this.gameObject);
            Debug.Log("collider mine");
        }


    }
}
