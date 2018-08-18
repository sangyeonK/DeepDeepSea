using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCtrl : MonoBehaviour {
    private GameObject player;


    void Update () {
        // 캐릭터를 중심으로 스크린오브젝트 세로이동
        Vector3 pos = transform.position;
        pos.y = player.transform.position.y - 5.0f;
        transform.position = pos;
	}

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
