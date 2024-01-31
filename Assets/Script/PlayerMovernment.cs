using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float maxJumpForce = 20f;
    public float chargeRate = 20f;
    public float moveSpeed = 5f;
    public float knockbackForce = 10f; // The force of the knockback
    private float currentJumpForce;
    private bool isJumping = false;
    private bool isCharging = false;
    private Rigidbody2D rb;
    private Vector3 originalScale; // Store the original scale
    private Animator anim; // Animator component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // Get the original scale
        anim = GetComponent<Animator>(); // Get the Animator component
    }

    void Update()
    {
        // Start charging jump when button is pressed
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isCharging = true;
            currentJumpForce = 0f;
            rb.velocity = Vector2.zero; // Stop the character immediately
        }

        // Charge up jump force while button is held
        if (isCharging)
        {
            currentJumpForce += chargeRate * Time.deltaTime;
            currentJumpForce = Mathf.Min(currentJumpForce, maxJumpForce);
        }

        // Jump when button is released
        if (Input.GetButtonUp("Jump") && isCharging)
        {
            rb.AddForce(new Vector2(0f, currentJumpForce), ForceMode2D.Impulse);
            isJumping = true;
            isCharging = false;
        }

        // Horizontal Movement
        if (!isCharging) // Only allow movement if not charging jump
        {
            float moveX = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

            // Flip character based on direction
            if (moveX > 0)
            {
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z); // Face right
            }
            else if (moveX < 0)
            {
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z); // Face left
            }
        }

        // Set the "Jump" parameter in the Animator
        anim.SetBool("Jump", isJumping);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("UI"))
        {
            isJumping = false;
            // Determine the direction of the knockback
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            Debug.Log("Knockback direction: " + knockbackDirection);
            // Apply the knockback force horizontally
            rb.AddForce(new Vector2(knockbackDirection.x * knockbackForce, 0f), ForceMode2D.Impulse);
        }
    }
}
