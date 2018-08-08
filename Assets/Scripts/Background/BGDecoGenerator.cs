using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGDecoGenerator : MonoBehaviour {

    public GameObject[] BGDecorations = null;
    public float minStartPositionY = -19.2f;
    public float maxStartPositionY = 0.0f;
    public float minDiffOfMovementY = 0.0f;
    public float maxDiffOfMovementY = 5.0f;
    public float minMovementVelocity = 0.01f;
    public float maxMovementVelocity = 0.1f;
    IEnumerator Generate()
    {
        GameObject prefab = BGDecorations[Random.Range(0, BGDecorations.Length)];
        bool reverse = Random.Range(0, 2) == 1 ? true : false;  // 반대방향으로 이동하는 데코레이션 으로 생성할 지 판단

        float startPositionY = Random.Range(minStartPositionY, maxStartPositionY);
        float stopPositionY = startPositionY + Random.Range(minDiffOfMovementY, maxDiffOfMovementY);
        Vector3 startPosition = transform.position + new Vector3(10.8f, startPositionY);
        Vector3 stopPosition = transform.position + new Vector3(-10.8f, stopPositionY);
        GameObject newObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        BGDecoration bgDeco = newObject.GetComponent<BGDecoration>();
        bgDeco.SetProperty(startPosition, stopPosition, reverse, Random.Range(minMovementVelocity, maxMovementVelocity));


        yield return new WaitForSeconds(Random.Range(1f, 3f));

        StartCoroutine(Generate());
    }

    private void Start()
    {
        if (BGDecorations.Length != 0)
        {
            StartCoroutine(Generate());
        }
    }
}
