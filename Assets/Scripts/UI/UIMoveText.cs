using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Util;

public class UIMoveText : MonoBehaviour {

    Text depth;

    void Awake () {
        depth = GetComponent<Text>();
	}

    public void UpdateDepth(int depth)
    {
        string zoneName = Define.PELAGIC.GetName(depth);
        this.depth.text = string.Format("{0} {1}M", zoneName, depth);
    }
	
	
}
