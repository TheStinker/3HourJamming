using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Member;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    public AudioClip starve;
    public AudioClip ranover;
    public AudioClip caught;
    private AudioSource source;
    public int trashCollected = 0;
    private Animator animator;
    public Rigidbody rb;
    private Vector2 movement;
    private PlayerInput playerInput;
    private Interactable interactable;
    private Quaternion savedRotation;
    public bool isMoving;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        isMoving = true;
        savedRotation = transform.rotation;
        rb = GetComponent<Rigidbody>(); 
        animator = GetComponent<Animator>();
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
        rb.velocity = isMoving ? speed * new Vector3(movement.x, 0, movement.y) : Vector3.zero;
        if (movement == Vector2.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(rb.velocity.normalized);
        targetRotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            360 * Time.fixedDeltaTime * speed
            );
        rb.MoveRotation(targetRotation);
    }
    public void Starve()
    {  
        playerInput.DeactivateInput();
        transform.rotation = savedRotation;
        source.clip = starve;
        source.Play();
        animator.Play("starve");
        CameraFollower.Instance.ZoomIn();
    }
    public void Fat()
    {
        playerInput.DeactivateInput();
        transform.rotation = savedRotation;
        animator.Play("fat");
        CameraFollower.Instance.ZoomIn();
    }
    public void Caught()
    {
        playerInput.DeactivateInput();
        transform.rotation = savedRotation;
        source.clip = caught;
        source.Play();
        animator.Play("caught");
        CameraFollower.Instance.ZoomIn();
    }
    public void Ranover()
    {
        playerInput.DeactivateInput();
        //transform.rotation = savedRotation;
        source.clip = ranover;
        source.Play();
        animator.Play("ranover");
        CameraFollower.Instance.ZoomIn();
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
