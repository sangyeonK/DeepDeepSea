using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour {
	public float positionX;
	private const float startPositionY = -14.0f;
	private const float endPositionY = 11.6f;
	// Update is called once per frame
	void Update () {

		transform.Translate(0, GameManager.Instance.BackgroundSpeed, 0);

		if( transform.localPosition.y > endPositionY)
		{
			transform.localPosition = new Vector3(positionX,startPositionY,0);
		}
	}
}
