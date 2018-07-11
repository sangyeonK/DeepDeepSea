using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
    public float moveSpeed = 2.0f;

    private void Update()
    {

        if(Input.GetKey(KeyCode.Space)) {
            MoveRevers();
        }
        // transform.Translate(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime,
        //     moveSpeed*Input.GetAxis("Vertical") * Time.deltaTime,
        //     0f);
    }

    private void MoveRevers() {
        transform.Translate(moveSpeed * -1 * Time.deltaTime, 0f, 0f);
    }

}
