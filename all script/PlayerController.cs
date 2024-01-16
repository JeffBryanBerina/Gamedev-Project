using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 300f;
    public float runSpeed = 500f;
    
    public float jumpImpulse=10f;
  
    Vector2 moveInput;
    TouchingDirections touchingdirections;

    Rigidbody2D rb;
    Animator animator;

    [SerializeField]
    public bool _isFacingRigth = true;
    public bool IsFacingRigth {
        get
        {
            return _isFacingRigth;
        }

        private set//flip the value
        {
            if (_isFacingRigth != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRigth = value;
        }
    }
    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingdirections.IsOnWall)
                {


                    if (IsRunning)
                    {
                        return runSpeed;
                    }
                    else
                    {
                        return walkSpeed;
                    }

                }
                else
                {
                    return 0;//idle speed
                }

            }
           
          
            else
            {
                return 0;//idle speed
            }
        }
    }

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);

        }
    }



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingdirections = GetComponent<TouchingDirections>();
    }


   
    [SerializeField]
    private bool _isRunning = false;
    

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
          _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning,value);
        }
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x*CurrentMoveSpeed *Time.fixedDeltaTime, rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
        
    }

    public void onMove(InputAction.CallbackContext context)
    {
        moveInput= context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        setFacingDirection(moveInput);

    }

    private void setFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x>0 && !IsFacingRigth)
        {
            //face to the rigth

            IsFacingRigth = true;
        }
        else if (moveInput.x < 0 && IsFacingRigth)
        {
            //face to the rigth
            IsFacingRigth = false;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }
    
   
  


    public void onRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning= false;
        }
    }

    public void onJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingdirections.IsGrounded &&CanMove)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x,jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attack);
        }
    }
  
}
