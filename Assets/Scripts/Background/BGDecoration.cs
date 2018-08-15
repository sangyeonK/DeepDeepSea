using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGDecoration : MonoBehaviour {

    private Vector2 movementDirection;

	public void SetProperty(Vector2 startPosition, Vector2 stopPosition, bool reverse, float velocity)
    {
        if (reverse)
        {
            this.transform.position = stopPosition;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.flipX = true;
            this.movementDirection = (startPosition - stopPosition) * velocity;
        }
        else
        {
            this.transform.position = startPosition;
            this.movementDirection = (stopPosition - startPosition) * velocity;
        }
    }

    void Update () {
        transform.Translate(this.movementDirection * Time.deltaTime);
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
