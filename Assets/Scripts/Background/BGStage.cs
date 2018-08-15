using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BGStage : MonoBehaviour {

    public int stageareaCount = 2;
    private LayerMask screenBorder;
    List<GameObject> leftRocksInChild;
    List<GameObject> rightRocksInChild;
    const float posX_stopLeftRock = -5.4f;
    const float posX_stopRightRock = 5.4f;

    const float posX_rockStart = 7.4f;
    const float posX_rockStop = 5.4f;
    const float limit_rockTranslated = posX_rockStart - posX_rockStop;
    float rockTranslatedPosition;
    float rockTranslateSpeed;
    // Use this for initialization
    void Start () {
        rockTranslatedPosition = GameManager.Instance.BackgroundRockTranslated;
        screenBorder = LayerMask.NameToLayer("ScreenBorder");
        var transformsInChild = GetComponentsInChildren<Transform>();

        leftRocksInChild = transformsInChild
            .Where(trans =>
            {
                if (trans.gameObject.CompareTag("Background_LeftRock"))
                {
                    trans.localPosition = new Vector2((posX_rockStart - rockTranslatedPosition) * -1, 0f);
                    return true;
                }
                return false;
            })
            .Select(trans => trans.gameObject)
            .ToList();

        rightRocksInChild = transformsInChild
            .Where(trans =>
            {
                if (trans.gameObject.CompareTag("Background_RightRock"))
                {
                    trans.localPosition = new Vector2((posX_rockStart - rockTranslatedPosition), 0f);
                    return true;
                }
                return false;
            })
            .Select(trans => trans.gameObject)
            .ToList();
    }
	
	// Update is called once per frame
	void Update () {
        MoveRocksInChild();
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
        float moveY = (stageareaCount - 1) * BackgroundCtrl.BACKGROUND_HEIGHT * -1f;
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

    private void MoveRocksInChild()
    {
        if (rockTranslatedPosition >= limit_rockTranslated)
            return;

        float moveX = Time.deltaTime * rockTranslateSpeed;
        rockTranslatedPosition += moveX;
        if (rockTranslatedPosition > limit_rockTranslated)
            rockTranslatedPosition = limit_rockTranslated;


        Vector2 left = new Vector2((posX_rockStart - rockTranslatedPosition) * -1, 0f);
        Vector2 right = new Vector2((posX_rockStart - rockTranslatedPosition), 0f);
        foreach (var rock in leftRocksInChild)
        {
            rock.transform.localPosition = left;
        }
        foreach (var rock in rightRocksInChild)
        {
            rock.transform.localPosition = right;
        }

        GameManager.Instance.BackgroundRockTranslated = rockTranslatedPosition;
    }

    public void SetRockTranslateSpeed(float speed)
    {
        rockTranslateSpeed = speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
