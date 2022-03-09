using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossOrientation : MonoBehaviour
{
    public AIPath aiPath;

    // Update is called once per frame
    void Update()
    {
     //changing direction
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1.1f, .8f, 1f);
        } else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1.1f, .8f, 1f);
        }
    //the # in front of f is the scale
    }
}
