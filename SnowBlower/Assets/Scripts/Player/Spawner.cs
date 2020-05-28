using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Worker
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] List<string> objects;

    [SerializeField] float rate = .5f;

    float _timePassed;

    void Update()
    {
        if(IsWorking)
        {
            if(Time.time >= _timePassed + rate)
            {
                for (int i = 0; i < spawnPoints.Count; i++)
                    ObjectPooler.inst.Spawn(objects[Random.Range(0, objects.Count)], spawnPoints[i].position, spawnPoints[i].rotation);
                _timePassed = Time.time;
            }
        }
    }
}
