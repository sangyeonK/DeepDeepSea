using System.Collections; using System.Collections.Generic; using UnityEngine;  public class leftObs : MonoBehaviour {



    public float moveSpeed;     public int x;     public int y;      private void Start()
    {
        x = Random.Range(1, 3);
    }      void Update()     {                 transform.position =            new Vector3(x * Mathf.PingPong(Time.time,3), transform.position.y, transform.position.z);       }   }   