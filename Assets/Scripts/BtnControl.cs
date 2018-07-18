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
    }
 
    public void OnPointerUp (PointerEventData eventData)
    {
        isBtnDown = false;
    }
}
