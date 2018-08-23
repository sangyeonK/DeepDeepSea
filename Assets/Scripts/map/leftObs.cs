using System.Collections; using System.Collections.Generic; using UnityEngine;  public class leftObs : MonoBehaviour {


    public float moveSpeed;     public int x;     public int y;        void Update()     {          x = 3;          transform.position =            new Vector3(x * Mathf.PingPong(Time.time, 3), transform.position.y, transform.position.z);       }    }  