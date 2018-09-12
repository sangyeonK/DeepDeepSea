using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updown : MonoBehaviour {
 	private int x;
	// Use this for initialization
	void Start () {
		x = Random.Range(1, 2);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position =
           new Vector3(x * Mathf.PingPong(Time.time, 2), this.transform.position.y, transform.position.z);

	}
}
