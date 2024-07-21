using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    #region 
    private static CameraFollower instance;
    public static CameraFollower Instance
    {
        get 
        { 
            if (instance == null)
            {
                instance = FindAnyObjectByType<CameraFollower>();
            }
            return instance; 
        }
    }
    #endregion 
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
    }
    public void ZoomIn()
    {
        animator.Play("zoomin");
    }
    public void ZoomOut()
    {
        animator.Play("zoomout");
    }
}
