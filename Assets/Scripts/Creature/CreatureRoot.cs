using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureRoot : MonoBehaviour {

    public enum ANIMATION_PATTERN
    {
        PATTERN_1 =1,
        PATTERN_2,
        PATTERN_3
    }

    public Vector2 minVelocity;
    public Vector2 maxVelocity;
    public Creature child;
    public ANIMATION_PATTERN[] patterns;

    private Vector2 velocity;


    private void Start()
    {
        float x = Random.Range(minVelocity.x, maxVelocity.x);
        float y = Random.Range(minVelocity.y, maxVelocity.y);

        velocity = new Vector2(x, y);
    }

    public void SetAnimationDirection(bool toRight)
    {
        if (patterns.Length > 0)
        {
            ANIMATION_PATTERN pattern = patterns[Random.Range(0, patterns.Length)];
            string patternName = string.Format("{0}_{1}", (toRight ? "ToRight" : "ToLeft"), (int)pattern);
            child.SetAnimationPattern(patternName);
        }
    }

    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
    }
}
