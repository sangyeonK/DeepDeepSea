using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	public float moveSpeed;
    float moveSpeedBoostTime = 0.0f;

	// Update is called once per frame
	void Update () {
        if (moveSpeedBoostTime > 0.0f)
        {
            GameManager.Instance.moveSpeedBoost = true;
            moveSpeedBoostTime = Mathf.Max(moveSpeedBoostTime - Time.deltaTime, 0.0f);
        }
        else
        {
            GameManager.Instance.moveSpeedBoost = false;
        }

        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

    }

    public void GetItem(Item.ItemKind itemKind)
    {
        switch (itemKind) {
            case Item.ItemKind.SPEED_BOOST:
                moveSpeedBoostTime = 5.0f;
                break;
        }

    }
}
