using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_wall : MonoBehaviour {

    public float moveSpeed;
    public int x;
    public int y;
	// Update is called once per frame
	void Update () {
        moveSpeed = Random.Range(6.0f, 6.5f);
        transform.Translate(x, y* moveSpeed * Time.deltaTime, 0);
    }
    /* private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
*/
}
