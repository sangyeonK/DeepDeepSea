using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPreset : MonoBehaviour {

    private LayerMask screenBorder;
    private MapManager mapManager;

    void Start () {
        screenBorder = LayerMask.NameToLayer("ScreenBorder");
    }

    public void SetMapManager(GameObject mapManager)
    {
        this.mapManager = mapManager.GetComponent<MapManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision!!");
        if (collision.gameObject.layer == screenBorder.value)
        {
            Debug.Log("Collision!!");
            mapManager.InstantiateMapType(collision.transform.position.y - Define.SCREEN_HEIGHT);
        }
    }
}
