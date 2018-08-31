using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenerate : MonoBehaviour {
 

    public GameObject [] obj;
	private int rand;
	// Use this for initialization
	void Start () {
		int rand = Random.Range(0,obj.Length);
		Instantiate(obj[rand],transform.position,Quaternion.identity);
		
	}

}
