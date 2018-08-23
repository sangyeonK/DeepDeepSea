using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParallaxBackgroundCtrl : MonoBehaviour
{
    public Camera internalCamera;
    public int parallexRatio = 8;
    private GameObject player;

    List<GameObject> leftRocksInChild;
    List<GameObject> rightRocksInChild;
    const float posX_stopLeftRock = -5.4f;
    const float posX_stopRightRock = 5.4f;

    const float posX_rockStart = 7.4f;
    const float posX_rockStop = 5.4f;
    const float limit_rockTranslated = posX_rockStart - posX_rockStop;
    float rockTranslatedPosition;
    public float rockTranslateSpeed = 0.1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rockTranslatedPosition = GameManager.Instance.BackgroundRockTranslated;
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
    void Update()
    {
        MoveRocksInChild();
        internalCamera.transform.localPosition = new Vector3(0.0f, CalcParallelCameraPosY(), internalCamera.transform.localPosition.z);
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

    private float CalcParallelCameraPosY()
    {
        float posY = player.transform.position.y / 4;
        return posY % Define.SCREEN_HEIGHT;
        
    }
}
