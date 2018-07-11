using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	public float moveSpeed;
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * moveSpeed * Time.deltaTime);
	}
}
