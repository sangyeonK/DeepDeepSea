using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGStage : MonoBehaviour {

	public enum StageNumber
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage5
    }

    public StageNumber stageNumber;

    private LayerMask screenBorder;
    public int AreaIndex
    {
        get;
        set;
    }

    private void Start()
    {
        screenBorder = LayerMask.NameToLayer("ScreenBorder");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == screenBorder.value)
        {
            SendMessageUpwards("OnStageAreaEnter", AreaIndex);
        }
    }




}
