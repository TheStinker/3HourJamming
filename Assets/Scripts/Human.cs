using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour {


    [SerializeField] private LayerMask obstaclesLayerMask;
    [SerializeField] private MeshFilter navMeshArea;
    [SerializeField] private float minMoveDistance;
    [SerializeField] private float maxMoveDistance;
    [SerializeField] private float moveCooldown;


    private NavMeshAgent agent;
    private Vector3 destination;
    private Vector3 boundsMin;
    private Vector3 boundsMax;
    private float moveCooldownTimer;
    private bool isMoving = false;
    private bool isChasing = false;


    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        boundsMin = navMeshArea.mesh.bounds.min;
        boundsMax = navMeshArea.mesh.bounds.max;
    }

    private void Update() {
        if (isChasing) {
            return;
        }

        if (isMoving) {
            if (Vector3.Distance(transform.position, destination) < 0.1f) {
                isMoving = false;
            }
        } else {
            moveCooldownTimer -= Time.deltaTime;

            if (moveCooldownTimer <= 0) {
                Vector3 randomDirection = Random.insideUnitSphere * maxMoveDistance;
                if (randomDirection.magnitude < minMoveDistance) {
                    randomDirection = randomDirection.normalized * minMoveDistance;
                }
                Vector3 testDestination = transform.position + randomDirection;

                if (NavMesh.SamplePosition(testDestination, out NavMeshHit hit, 1.0f, NavMesh.AllAreas)) {
                    if (PositionValid(hit.position)) {
                        destination = hit.position;
                        agent.SetDestination(destination);
                        
                        isMoving = true;
                        moveCooldownTimer = moveCooldown;
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        destination = other.transform.position;
    }

    private bool PositionValid(Vector3 position) {
        // Position clear
        if (Physics.Raycast(position, Vector3.up, 2f, obstaclesLayerMask)) {
            //Debug.Log("Occupied " + position);
            return false;
        }

        // In yard bounds
        if (position.x > boundsMax.x || position.x < boundsMin.x ||
            position.z > boundsMax.z || position.z < boundsMin.z) {
            //Debug.Log("Out of bounds " + position);
            return false;
        }

        return true;
    }

}
