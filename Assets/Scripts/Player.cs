using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    public int trashCollected = 0;
    private Animator animator;
    private Rigidbody rb;
    private Vector2 movement;
    private PlayerInput playerInput;
    private Interactable interactable;
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        playerInput = GetComponent<PlayerInput>();
    }
    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
    public void OnInteract() 
    {
        interactable?.Interact(this);
    }
    private void FixedUpdate()
    {
        rb.velocity = speed * new Vector3(movement.x, 0, movement.y);
        if (movement == Vector2.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(rb.velocity.normalized);
        targetRotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            360 * Time.fixedDeltaTime * speed
            );
        rb.MoveRotation(targetRotation);
    }
    public void Caught()
    {
        animator.Play("caught");
        // Camera Logic
    }
    public void Ranover()
    {
        animator.Play("ranover");
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            this.interactable = interactable;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        this.interactable = null;
    }
}
