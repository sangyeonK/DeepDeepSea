using System.Collections;
using UnityEngine;


public class MapManager : MonoBehaviour {


    public GameObject[] mapList = null;
    float interval = 0;
    void Start()
    {

    }

    
    void Update()
    {
        interval += Time.deltaTime;
        if (interval > 6.6f)
        {
            GameObject obj = Instantiate(mapList[Random.Range(0, 2)]);
            obj.transform.position = new Vector3(-0.3f, -8.45f, 0);
            interval = 0;
        }
    }


}
