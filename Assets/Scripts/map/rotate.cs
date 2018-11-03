using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{

    private int rotFl;
    // Use this for initialization
     
    private void Start()
    {
        rotFl= Random.Range(1,5);
           
        switch(rotFl){
            case 1:
                rotFl = Random.Range(1,60);
                break;

            case 2:
                rotFl = Random.Range(61,120);
                break;
               
            case 3:
                rotFl = Random.Range(121,180);
                break;

            case 4:
                rotFl = Random.Range(181,270);
                break;

            case 5:
                rotFl = Random.Range(-180,-271);
                break;

        }
    }
      
      
    void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * 50, rotFl));
                 /*if( check something so that switch occurs){
          transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * 50, -rotFl));    
           }*/
    }

}
