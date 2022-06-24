using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private Animator _anim;
    
    // movement parameters
    private float _horizontalInput;
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float jumpForce = 6f;
    private const float JumpPressedRememberTime = 0.05f;
    private float _jumpPressedRemember;

    private bool _isAttacking;
    
    
    // GroundCheck parameters
    public Transform groundCheck;
    private bool _isGrounded;
    private const float GroundCheckRadius = 0.2f;
    public LayerMask whatIsGround;
    
    // Event Functions
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {   
       
        // Check if the player is grounded
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, GroundCheckRadius, whatIsGround);
        
        // Attack
        if (Input.GetButtonDown("Fire1") && _isGrounded) Attack();
        
        
        // Jump
        _jumpPressedRemember -= Time.deltaTime;
        if (Input.GetButtonDown("Jump")) { _jumpPressedRemember = JumpPressedRememberTime; }
        if (_jumpPressedRemember > 0 && _isGrounded) { _rb.velocity = new Vector2(_rb.velocity.x, jumpForce); }
        
        SpriteAnimation();
    }

    private void FixedUpdate()
    {   
        
        // Run
        _horizontalInput = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(_horizontalInput*speed, _rb.velocity.y);
    }

    private void Attack()
    {
        _anim.SetTrigger("Attack");
        _rb.velocity = Vector2.zero;

    }
    
    private void SpriteAnimation()
    {
        if (_horizontalInput > 0) _sprite.flipX = false;
        else if (_horizontalInput < 0) _sprite.flipX = true;
        
        if (_rb.velocity.x != 0) _anim.SetBool("Run", true);
        else _anim.SetBool("Run",false);

        if (!_isGrounded) _anim.SetBool("Jump",true);
        else _anim.SetBool("Jump",false);
    }
}
