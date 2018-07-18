using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour {

    public float speed = 0.1f;          //Speed of the scrolling

    void Update()
    {
        //Keep looping between 0 and 1
        float y = Mathf.Repeat(Time.time * speed, 1);
        //Create the offset
        Vector2 offset = new Vector2(0, y);
        //Apply the offset to the material
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
