using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    private bool generateTargetPos;
    Vector2 TargetPos;
    private float waitTime = 2f;
    private float currentWaitTime = 2f;
    private float moveTime = 2f;

    private bool ToWalk = false;

    private void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    public void WalkAround()
    {
        if (currentWaitTime <= 0)
        {
            parent.transform.Translate(TargetPos * Time.deltaTime);

            moveTime -= Time.deltaTime;
            if(moveTime <= 0)
            {
                currentWaitTime = waitTime;
            }
        }
        else if (currentWaitTime > 0)
        {
            currentWaitTime -= Time.deltaTime;
            if (currentWaitTime <= 0)
            {
                TargetPos = new Vector2(Random.Range(-2,2), 0);
                moveTime = 2f;
            }
        }
    }
}
