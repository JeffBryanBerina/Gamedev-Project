using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class KnigthScript : MonoBehaviour
{


    public float walkSpeed = 3f;
    public float walkStopRate = 0.06f;
    public DetectionZoneScript attackzone;


    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;

    public enum WalkableDirection { Rigth, Left}
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set { 
            if(_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Rigth)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if(value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                   
                }

            }

            _walkDirection = value;
        }
    }


    public bool _hasTarget = false;
    public bool HasTarget { get { return _hasTarget; } private set {

            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);

        } }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        if (CanMove)
        {
            // Keep the y-component of velocity unchanged
            rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
        }
        else
        {
            // Smoothly stop movement on the x-axis
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
    }

    void Update()
    {
        HasTarget = attackzone.detectedColliders.Count > 0;
    }

    private void FlipDirection()
    {
        if (WalkDirection==WalkableDirection.Rigth)
        {
            WalkDirection  = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Rigth;
        }
        else
        {
            Debug.LogError("Error! ");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
}
