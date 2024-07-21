using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject car;
    public bool forward;
    public float duration;
    private float interval;
    private Car currentlySpawned;
    private void Start()
    {
        interval = duration + Time.time;
    }
    private void Update()
    {
        if (Time.time >= interval) 
        {
            currentlySpawned = Instantiate(car).GetComponent<Car>();
            currentlySpawned.forward = forward;
            currentlySpawned.transform.Rotate(Vector3.up * (forward ? 0f : 180f));
            currentlySpawned.transform.position = transform.position;
            interval = Time.time + duration;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Car car) && currentlySpawned != car)
        {
            Destroy(car.gameObject);
        }
    }
}
