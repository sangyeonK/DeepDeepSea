using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{

      public float rotFl;
      // Use this for initialization
      void Start()
      {

      }
      
      void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * 50, rotFl));
                 /*if( check something so that switch occurs){
          transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * 50, -rotFl));    
           }*/
    }

}
