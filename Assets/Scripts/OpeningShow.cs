using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningShow : MonoBehaviour {

    public GameObject player;
    public GameObject mobile;
    public bool skipOpening = false;

    private void Start()
    {
        if (skipOpening)
        {
            StartCoroutine(Stop());
            return;
        }

        SwimFast();
    }

    void SwimFast()
    {
        player.GetComponent<Animator>().SetBool("SpeedBoost", true);
        player.GetComponent<Animator>().speed = 1.5f;
    }

    void SwimNormal()
    {
        player.GetComponent<Animator>().SetBool("SpeedBoost", false);
        player.GetComponent<Animator>().speed = 0.75f;
    }

    void PlayDivingSound()
    {
        GetComponent<AudioSource>().Play();
    }

    IEnumerator Stop()
    {
        yield return null;

        GameManager.Instance.StartGamePlay();
        GameObject.Destroy(gameObject);
    }
}
