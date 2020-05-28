using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Worker : MonoBehaviour
{
    public bool IsWorking { get; set; }

    public void SetWorkingSwap()
    {
        IsWorking = !IsWorking;
    }

    public void SetWorkingTrue()
    {
        IsWorking = true;
    }

    public void SetWorkingFalse()
    {
        IsWorking = false;
    }
}
