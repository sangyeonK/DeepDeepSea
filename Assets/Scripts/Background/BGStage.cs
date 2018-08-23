using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BGStage : MonoBehaviour {

    public int stageareaCount = 2;
    private LayerMask screenBorder;

    void Start () {
        screenBorder = LayerMask.NameToLayer("ScreenBorder");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == screenBorder.value)
        {
            RewindBackground();
        }
    }

    void RewindBackground()
    {
        float moveY = (stageareaCount - 1) * Define.SCREEN_HEIGHT * -1f;
        gameObject.transform.Translate(new Vector2(0.0f, moveY));
    }

    public void SetDisable()
    {
        GetComponent<EdgeCollider2D>().enabled = false;
        foreach(var x in GetComponentsInChildren<SpriteRenderer>() )
        {
            x.sortingOrder -= 1000;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
