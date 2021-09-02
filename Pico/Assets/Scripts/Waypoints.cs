using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    Transform[] waypoints;
    int leftDirection, rightDirection;
    Vector2 limit = new Vector2(150, - 50);
    void Start()
    {
        waypoints = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        if (waypoints[1].localPosition.y > limit.x)
        {
            leftDirection = -1;
        }
        else if (waypoints[1].localPosition.y < limit.y)
        {
            leftDirection = 1;
        }
        waypoints[1].Translate(Vector2.up * leftDirection);


        if (waypoints[2].localPosition.y > limit.x)
        {
            rightDirection = -1;
        }
        else if (waypoints[2].localPosition.y < limit.y)
        {
            rightDirection = 1;
        }
        waypoints[2].Translate(Vector2.up * rightDirection);
    }
}