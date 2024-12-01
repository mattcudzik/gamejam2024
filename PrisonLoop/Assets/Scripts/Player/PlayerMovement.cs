using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f; // Prędkość poruszania

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool freezePlayer = false;
    void Awake()
    {
        MiniGameBase.onMiniGameStart += FreezMovement;
        MiniGameBase.onMiniGameEnd += UnFreezMovement;
        GuardFOV.onCaught += FreezMovement;
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

        // Ruch oparty na czasie (płynność niezależnie od FPS)
        transform.Translate(movement * (moveSpeed * Time.deltaTime));
    }
}
