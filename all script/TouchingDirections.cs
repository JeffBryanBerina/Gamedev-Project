using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float cellingDistance = 0.05f;


    CapsuleCollider2D touchingcol;
    Animator animator;


    RaycastHit2D[] groundhit = new RaycastHit2D[5];

    RaycastHit2D[] wallHits = new RaycastHit2D[5];

    RaycastHit2D[] cellingHits = new RaycastHit2D[5];


    [SerializeField]
    private bool _isGrounded;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set { 
            _isGrounded = value;
            animator.SetBool(AnimationStrings.Ground, value);
        }
    }

    [SerializeField]
    private bool _isOnWall;

    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }



    [SerializeField]
    private bool _isOnCelling;
    private Vector2 wallCheckDirection=>gameObject.transform.localScale.x >0? Vector2.right:Vector2.left;

    public bool IsOnCelling
    {
        get
        {
            return _isOnCelling;
        }
        private set
        {
            _isOnCelling = value;
            animator.SetBool(AnimationStrings.isOnCelling, value);
        }
    }
    private void Awake()
    {
        touchingcol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();    
    }


    void FixedUpdate()
    {
        IsGrounded = touchingcol.Cast(Vector2.down, castFilter, groundhit, groundDistance)>0;
        IsOnWall = touchingcol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCelling = touchingcol.Cast(Vector2.up, castFilter, cellingHits, cellingDistance) > 0;
    }
}
