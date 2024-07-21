using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public bool forward;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    private void FixedUpdate()
    {
        rb.velocity = (forward ? Vector3.forward : Vector3.back) * speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.Ranover();
            GameManager.Instance.EndGame();
        }
    }
}
