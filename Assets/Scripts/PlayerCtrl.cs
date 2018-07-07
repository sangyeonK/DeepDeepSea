using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
    public float moveSpeed = 2.0f;

    private void Update()
    {
        transform.Translate(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime,
            moveSpeed*Input.GetAxis("Vertical") * Time.deltaTime,
            0f);
    }

}
