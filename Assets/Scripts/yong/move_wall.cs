using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_wall : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 1.2f * Time.deltaTime, 0);
    }
}
