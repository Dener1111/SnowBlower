using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : Worker
{
    [SerializeField] protected bool useGlobalSpeed;
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected Vector3 direction;

    float _lifetime;

    void Start()
    {
        if(useGlobalSpeed)
            speed = LevelController.inst.speed;
    }

    void Update()
    {
        if(IsWorking)
        {
            float fixSpeed = speed * Time.deltaTime;
            Vector3 speedDir = direction * fixSpeed;
            transform.Translate(speedDir);
        }
    }
}
