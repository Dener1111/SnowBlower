using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTrail : Worker
{
    [SerializeField] float lifetime = 5f;

    [SerializeField] float minimumVertexDistance = 0.1f; //minimum distance moved before a new point is solidified.

    [SerializeField] bool useGlobalSpeed;
    [SerializeField] Vector3 speed; //direction the points are moving

    LineRenderer _line;
    //position data
    [HideInInspector] public List<Vector3> points;
    Queue<float> _spawnTimes = new Queue<float>(); //list of spawn times, to simulate lifetime. Back of the queue is vertex 1, front of the queue is the end of the trail.
    //Length of this queue is one less than the number of positions in the linerenderer, since the linerenderer always has a non-solidified vertex at the object's position.

    void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _line.useWorldSpace = true;
        points = new List<Vector3>() { transform.position }; //indices 1 - end are solidified points, index 0 is always transform.position
        _line.SetPositions(points.ToArray());
    }

    void Start()
    {
        IsWorking = true;

        if (useGlobalSpeed)
            speed.z = LevelController.inst.speed;
    }

    void AddPoint(Vector3 position)
    {
        points.Insert(1, position);
        _spawnTimes.Enqueue(Time.time);
    }

    void RemovePoint()
    {
        _spawnTimes.Dequeue();
        points.RemoveAt(points.Count - 1); //remove corresponding oldest point at the end
    }

    void Update()
    {
        if (IsWorking)
        {
            //cull based on lifetime
            while (_spawnTimes.Count > 0 && _spawnTimes.Peek() + lifetime < Time.time)
            {
                RemovePoint();
            }

            //move positions
            Vector3 diff = -speed * Time.deltaTime;
            for (int i = 1; i < points.Count; i++)
            {
                points[i] += diff;
            }

            //add new point
            if (points.Count < 2 || Vector3.Distance(transform.position, points[1]) > minimumVertexDistance)
            {
                //if we have no solidified points, or we've moved enough for a new point
                AddPoint(transform.position);
            }

            //update index 0;
            points[0] = transform.position;

            //save result
            _line.positionCount = points.Count;
            _line.SetPositions(points.ToArray());
        }
    }
}
