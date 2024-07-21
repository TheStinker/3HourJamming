using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCaller : MonoBehaviour
{
    public UnityEvent doAction;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() == null) return;
        doAction.Invoke();
    }
}
