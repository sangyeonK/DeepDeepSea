using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCtrl : MonoBehaviour {
    public GameObject player;
    public GameObject background;

    private Animator bgAnimator;
    private int bgAnimatorParamID_playerVerticalSpeed;

    private void Awake()
    {
        bgAnimator = background.GetComponent<Animator>();
        bgAnimatorParamID_playerVerticalSpeed = Animator.StringToHash("playerVerticalSpeed");
        Cursor.visible = false;
    }

    void Update () {
        // 캐릭터를 중심으로 스크린오브젝트 세로이동
        Vector3 pos = transform.position;
        pos.y = player.transform.position.y - 5.0f;
        transform.position = pos;

        // 배경 오브젝트 이동
        bgAnimator.SetFloat(bgAnimatorParamID_playerVerticalSpeed, GameManager.Instance.PlayerVerticalSpeed);
	}
}
