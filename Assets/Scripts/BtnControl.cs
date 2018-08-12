using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnControl : MonoBehaviour,  IPointerDownHandler, IPointerUpHandler {

	private bool isBtnDown = false;
        
    public void OnPointerDown (PointerEventData eventData)
    {
        isBtnDown = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player.GetComponent<Character>().death)
        {
            GameManager.Instance.isSpeedMode = isBtnDown;
            Animator ani = player.GetComponentInChildren<Animator>();
            ani.SetBool("SpeedBoost", true);
        }
    }
 
    public void OnPointerUp (PointerEventData eventData)
    {
        isBtnDown = false;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player.GetComponent<Character>().death)
        {
            GameManager.Instance.isSpeedMode = isBtnDown;
            Animator ani = player.GetComponentInChildren<Animator>();
            ani.SetBool("SpeedBoost", false);
        }
    }
}
