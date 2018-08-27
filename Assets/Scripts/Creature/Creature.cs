using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

    public CreatureRoot root;

    void ResetRootPosition()
    {
        Vector3 pos = root.transform.localPosition;
        pos += transform.localPosition;
        root.transform.localPosition = pos;
    }

    public void SetAnimationPattern(string patternName)
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger(patternName);
        animator.speed = Random.Range(0.2f, 0.5f);

    }

    private void OnBecameInvisible()
    {
     //GameObject.Destroy(gameObject);
      //  GameObject.Destroy(root.gameObject);
    }

}
