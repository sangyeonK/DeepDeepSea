using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

    public GameObject item;

	IEnumerator Generate()
    {
        Instantiate(item,
            transform.localPosition + new Vector3(Random.Range(-5.0f, 5.0f), 0.0f),
            Quaternion.identity,
            transform);

        yield return new WaitForSeconds(Random.Range(1f, 3f));

        StartCoroutine(Generate());
    }

    private void Start()
    {
        StartCoroutine(Generate());
    }


}
