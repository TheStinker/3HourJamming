using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class TrashCan : MonoBehaviour, Interactable
{
    [SerializeField] private int trashPieces = 3;
    [SerializeField] private float timeBetweenEach = 1f;
    [SerializeField] private Collider trashcancollider;
    private Animator animator;
    private AudioSource source;
    private Vector3 savedPosition;
    private Transform savedParent;
    private Player player;
    private bool raccoonInbin;
    private float cooldown;
    private float delay;
    private void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (raccoonInbin && Time.time >= cooldown && trashPieces > 0)
        {
            player.trashCollected++;
            trashPieces--;
            if (trashPieces <= 0)
            {
                GetOut();
            }
            cooldown = Time.time + timeBetweenEach;
            
        }
    }
    public void Interact(Player player)
    {
        if (trashPieces <= 0 || Time.time < delay) return;
        delay = Time.time + 0.4f;
        this.player = player;
        if (!raccoonInbin)
        {
            Rummage();
        }
        else
        {
            GetOut();
        }   
    }
    private void Rummage()
    {
        trashcancollider.enabled = false;
        player.rb.position = transform.position + Vector3.up * 0.2f;
        player.isMoving = false;
        cooldown = Time.time + timeBetweenEach;
        source.Play();
        animator.Play("openlid");
        raccoonInbin = true;
    }
    private void GetOut()
    {
        trashcancollider.enabled = true;
        player.rb.position = player.rb.position + Vector3.back * 1f;
        player.isMoving = true;
        source.Stop();
        animator.Play("closelid");
        raccoonInbin = false;
    }
}
