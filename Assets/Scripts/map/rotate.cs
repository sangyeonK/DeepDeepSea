using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

    public GameObject go;
    float angle = 360.0f; // Degree per time unit
    float time = 1.0f; // Time unit in sec
  //  Vector3 axis = Vector3.up; // Rotation axis, here it the yaw axis
    Vector3 axis2 = Vector3.forward;
    private void Update()
    {
        go.GetComponent<Transform>().RotateAround(Vector3.zero, axis2, angle * Time.deltaTime / time);
    }

}
