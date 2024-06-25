using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Initialized fields
    [SerializeField] private Rigidbody2D rb;

    // Private Fields
    private float horizontal;
    private float vertical;

    // Public Movement Fields
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 10.0f;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        Jump();
    }

    private void FixedUpdate()
    {
        HorizontalMovement();
    }

    /// <summary>
    /// Jump function that makes the player jump in game
    /// </summary>
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Checks if the player can jump
    /// </summary>
    /// <returns>A boolean that states if the player is allowed to jump</returns>
    private bool CanJump()
    {
        return true;
    }

    private void HorizontalMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
}
