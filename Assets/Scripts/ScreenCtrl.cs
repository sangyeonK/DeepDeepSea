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

    }

    void Update () {
        Vector3 pos = transform.position;
        pos.y = player.transform.position.y - 5.0f;
        transform.position = pos;

        bgAnimator.SetFloat(bgAnimatorParamID_playerVerticalSpeed, GameManager.Instance.PlayerVerticalSpeed);
	}
}
