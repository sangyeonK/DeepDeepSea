using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterMobile : MonoBehaviour {

    float minY = -10f;
    float maxY = 0f;
    public GameObject sprite;
    public float floatOnFactor = 0.25f;   // 값이 증가할수록 초반에 가파르게 떠오름
    public float floatOffFactor = 3.0f;   // 지정한 값이 클수록 빠르게 아래로 잠수
    private Vector3 rotationAngle;
    // Use this for initialization
    void Start () {
        rotationAngle = Vector3.forward * Random.Range(60f, 120f);
        transform.localPosition = new Vector2(0.0f, minY);

        sprite.GetComponent<SpriteRenderer>().enabled = false;
        GameManager.Instance.AddStartPlayHandler(OnStartPlay);
    }
	
	// Update is called once per frame
	void Update () {
        sprite.transform.Rotate(rotationAngle * Time.deltaTime);

        // speed mode 활성화 되면 떠오름..그렇지 않으면 가라앉음
        if (GameManager.Instance.isSpeedMode)
        {
            transform.Translate(0f, (maxY - transform.localPosition.y) * Time.deltaTime * floatOnFactor, 0f);
        }
        else
        {
            transform.Translate(0f, Time.deltaTime * floatOffFactor * -1f, 0f);
        }

        Vector3 currPosition = transform.localPosition;
        float clampedY = Mathf.Clamp(currPosition.y, minY, maxY);
        if (!Mathf.Approximately(clampedY, currPosition.y))
        {
            currPosition.y = clampedY;
            transform.localPosition = currPosition;
        }
    }

    void OnStartPlay()
    {
        sprite.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveStartPlayHandler(OnStartPlay);
    }
}
