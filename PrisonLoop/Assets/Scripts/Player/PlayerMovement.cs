using System;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f; // Prędkość poruszania

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool freezePlayer = false;
    private bool digging = false;
    void Awake()
    {
        MiniGameBase.onMiniGameStart += FreezMovement;
        MiniGameBase.onMiniGameEnd += UnFreezMovement;
        MiniGameBase.onDigisStarted += StartDigging;
        MiniGameBase.onDigiClosed += EndDigging;
        GuardFOV.onCaught += FreezMovement;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void EndDigging()
    {
        digging = false;
    }

    private void StartDigging()
    {
        digging = true;
    }

    public bool IsPlayerDigging()
    {
        return digging;
    }
    
    private void FreezMovement()
    {
        freezePlayer = true;
    }

    private void UnFreezMovement()
    {
        freezePlayer = false;
    }

    void FixedUpdate()
    {
        if(freezePlayer) return;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveX, moveY, 0f).normalized;

        if (movement.magnitude > 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
            
        }
        if (moveX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        // Ruch oparty na czasie (płynność niezależnie od FPS)
        transform.Translate(movement * (moveSpeed * Time.deltaTime));
    }
}
