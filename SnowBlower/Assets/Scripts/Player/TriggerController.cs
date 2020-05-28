using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerController : MonoBehaviour
{
    [SerializeField] LayerMask collideMask;
    [SerializeField] LayerMask snowMask;
    [SerializeField] LayerMask snowOffMask;
    [SerializeField] LayerMask finishMask;

    [SerializeField] UnityEvent crushEvent;
    [SerializeField] UnityEvent snowEvent;
    [SerializeField] UnityEvent snowOffEvent;
    [SerializeField] UnityEvent finishEvent;
    
    void OnTriggerEnter(Collider other)
    {
        if(collideMask.value == 1 << other.gameObject.layer)
        {
            crushEvent.Invoke();
        }
        else if(snowMask.value == 1 << other.gameObject.layer)
        {
            snowEvent.Invoke();
        }
        else if(snowOffMask.value == 1 << other.gameObject.layer)
        {
            snowOffEvent.Invoke();
        }
        else if(finishMask.value == 1 << other.gameObject.layer)
        {
            Debug.Log("finish");
            finishEvent.Invoke();
        }
    }
}
