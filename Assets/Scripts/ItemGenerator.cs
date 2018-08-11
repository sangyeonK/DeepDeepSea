using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

    public GameObject item;

	IEnumerator Generate()
    {
        Vector3 location = transform.position + new Vector3(Random.Range(-5.0f, 5.0f), -6.0f);
        Instantiate(item, location, Quaternion.identity);

        yield return new WaitForSeconds(Random.Range(1f, 3f));

        StartCoroutine(Generate());
    }

    private void Start()
    {
        StartCoroutine(Generate());
    }


}
