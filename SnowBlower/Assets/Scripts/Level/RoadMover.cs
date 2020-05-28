using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMover : Mover
{
    [SerializeField] List<Transform> objects;
    [SerializeField] List<GameObject> offables;

    [Space]
    [SerializeField] float rearrangeAfter;//distance needed to move object before rearrenge

    bool _activateOffables;

    float _dist;
    int _lastObject;

    void Start()
    {
        _activateOffables = true;

        if(useGlobalSpeed)
            speed = LevelController.inst.speed;

        if(objects.Count > 0)
            _lastObject = objects.Count - 1;
    }

    void Update()
    {
        if (IsWorking)
        {
            //calculate direction and speed for object translation
            float fixSpeed = speed * Time.deltaTime;
            Vector3 speedDir = direction * fixSpeed;

            foreach (var item in objects)
                item.Translate(speedDir);

            _dist += fixSpeed;
            if(_dist >= rearrangeAfter)
                Rearrange();
        }
    }

    void Rearrange()
    {
        if(objects.Count > 1)
        {
            //move instance of object accounting amount in specific direction
            offables[_lastObject].SetActive(_activateOffables);

            objects[_lastObject].position += -direction * (rearrangeAfter * objects.Count);

            //if current/last object is first in list we make it last
            //otherwise just move it up the list
            if(_lastObject == 0)
                _lastObject = objects.Count - 1;
            else
                _lastObject--;
        }

        //setting _dist to zero for new object to count its distance 
        _dist = 0;
    }

    public void Offables()
    {
        _activateOffables = !_activateOffables;
    }

    [ContextMenu("Set Active Offables")]
    public void SetActiveOffables()
    {
        foreach (var item in offables)
            item.SetActive(!item.activeSelf);
    }
}
