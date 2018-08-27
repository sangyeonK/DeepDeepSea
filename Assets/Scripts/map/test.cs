using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	
      public float rotFl;
     void Start(

	 ){
		 transform.localScale += new Vector3(0.5F, 0, 0);
	 }
      
      void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * 50, rotFl));
        /*
		if( check something so that switch occurs){
          transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * 50, -rotFl));    
           }
		
		*/
    }
}
