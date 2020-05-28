using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : Worker
{
    [SerializeField] CarController target;
    [SerializeField] float turnSpeed = 1.5f;
    [SerializeField] int pointToFollow;

    void Update()
    {
        if(target.trail.points.Count > pointToFollow && IsWorking)
        {
            transform.position = Vector3.Lerp(transform.position, target.trail.points[pointToFollow], turnSpeed * Time.deltaTime);
            transform.LookAt(target.transform);
        }
    }
}
