using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureGenerator : MonoBehaviour {

    public CreatureRoot[] creatures;

    IEnumerator Generate()
    {
        if (creatures.Length > 0) {
            GameObject prefab = creatures[Random.Range(0, creatures.Length)].gameObject;
            float x = Random.Range(-5.0f, 5.0f);
            bool toRight = x < 0f ? true : false;
            Vector3 location = transform.position + new Vector3(x, 19.2f / 2 * -1);
            GameObject creature = Instantiate(prefab, location, Quaternion.identity);
            creature.GetComponent<CreatureRoot>().SetAnimationDirection(toRight);

            yield return new WaitForSeconds(Random.Range(1f, 3f));

            StartCoroutine(Generate());
        }
    }

    void Start ()
    {
        GameManager.Instance.AddStartPlayListener(OnStartPlay);
    }

    void OnStartPlay()
    {
        StartCoroutine(Generate());
    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveStartPlayListener(OnStartPlay);
    }
}
