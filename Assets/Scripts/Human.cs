using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour {


    private NavMeshAgent agent;


    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerStay(Collider other) {
        agent.SetDestination(other.transform.position);
    }

}
