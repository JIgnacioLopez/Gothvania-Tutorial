using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private Animator _anim;
    // Cini sdbhsadbhjasnj
    // movement parameters
    private float _horizontalInput;
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float jumpForce = 6f;
    private float jumpPressedRememberTime = 0.2f;
    private float AttackPressedRememberTime = 0.1f;
    private float jumpPressedRemember, attackPressedRemember;

    
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
        attackPressedRemember -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1")) Attack();
        // Jump
        jumpPressedRemember -= Time.deltaTime;
        if (Input.GetButtonDown("Jump")) { jumpPressedRemember = jumpPressedRememberTime; }
        if (jumpPressedRemember > 0 && _isGrounded) { _rb.velocity = new Vector2(_rb.velocity.x, jumpForce); }
        
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
