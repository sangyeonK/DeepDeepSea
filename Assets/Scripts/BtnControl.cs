using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnControl : MonoBehaviour,  IPointerDownHandler, IPointerUpHandler {

	private bool isBtnDown = false;
 
    private void Update()
    {

        GameManager.Instance.isSpeedMode = isBtnDown;

    }
        
    public void OnPointerDown (PointerEventData eventData)
    {
        isBtnDown = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Animator ani = player.GetComponent<Animator>();
        ani.SetBool("SpeedBoost", true);
    }
 
    public void OnPointerUp (PointerEventData eventData)
    {
        isBtnDown = false;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Animator ani = player.GetComponent<Animator>();
        ani.SetBool("SpeedBoost", false);
    }
}
