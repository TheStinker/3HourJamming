using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour, Interactable
{
    [SerializeField] private int trashPieces = 3;
    [SerializeField] private float timeBetweenEach = 1f;
    private Animator animator;
    private Vector3 savedPosition;
    private Transform savedParent;
    private Player player;
    private bool raccoonInbin;
    private float cooldown;
    private float delay;
    private void Start()
    {
        animator = GetComponent<Animator>();
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
        if (trashPieces <= 0 && Time.time >= delay) return;
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
        savedParent = player.transform.parent;
        savedPosition = player.transform.position;
        player.transform.SetParent(transform, true);
        player.transform.position = transform.position + Vector3.up * 0.2f;
        cooldown = Time.time + timeBetweenEach;
        animator.Play("openlid");
        raccoonInbin = true;
    }
    private void GetOut()
    {
        player.transform.position = player.transform.position + Vector3.back * 0.5f;
        player.transform.SetParent(savedParent, true);
        animator.Play("closelid");
        raccoonInbin = false;
    }
}
