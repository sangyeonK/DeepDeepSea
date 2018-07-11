using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//https://www.youtube.com/watch?v=eMpI1eCsIyM

public class GlobalFlock: MonoBehaviour {

    public GameObject fishPrefab;
    public GameObject goalPrefab;

    public static int tankSize = 5;
    static int numFish = 10;

    public static GameObject[] allFish = new GameObject[numFish];

    public static Vector3 goalPos = Vector3.zero;


	// Use this for initialization
	void Start () {
		for (int i=0; i< numFish; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-tankSize, tankSize),
                Random.Range(-tankSize, tankSize),
                Random.Range(-tankSize, tankSize));
            Quaternion quaternion = Quaternion.identity;
            quaternion.eulerAngles = new Vector3(0, 90, 0);


            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, quaternion);
        }

	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range(0, 10000) < 50)
        {
            goalPos = new Vector3(Random.Range(-tankSize, tankSize),
                Random.Range(-tankSize, tankSize),
                Random.Range(-tankSize, tankSize));

            goalPrefab.transform.position = goalPos;
        }
	}
}
