using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : Worker
{
    [SerializeField] Camera cam;
    [SerializeField] Transform car;
    public StaticTrail trail;
    [SerializeField] float turnSpeed;

    Vector3 _mousePos;
    Vector3 _targetPos;
    float _posX;
    float _posZ = 6.5f;//how far is target for car to turn to
    //smaller value - bigger turn angle

    void Update()
    {
        if(IsWorking)
        {
            if(cam)
            {
                GetPos();
            }

            if(car)
            {
                MoveCar();
                TurnCar();
            }
        }
    }

    void GetPos()
    {
        //getting screen positin of mouse/finger
        //translating to world point
        if(Input.GetMouseButton(0))
        {
            _mousePos = Input.mousePosition;
            _mousePos.z = 10;
            _posX = cam.ScreenToWorldPoint(_mousePos).x;
        }
    }

    void MoveCar()
    {
        _targetPos = Vector3.right * _posX;
        
        car.position = Vector3.Lerp(car.position, _targetPos, turnSpeed * Time.deltaTime);
    }

    void TurnCar()
    {
        //turn car to moving direction
        _targetPos.z = _posZ;
        transform.LookAt(_targetPos);
    }
}
