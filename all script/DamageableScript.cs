using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableScript : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    [SerializeField]
    private int _health = 100;


    public int Health
    {
        get
        {
            return _health;

        }
        set
        {
            _health = value;
            if (_health < 0)
            {
                IsAlive = false;
            }
        }
    }



    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit =0;
    public float invicibilityTime=0.25f;

    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("iSaLIVE SET "+value);
        }
       }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invicibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
        hit(10);
    }
    public void hit(int damage)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible= true;
        }

    }
}
