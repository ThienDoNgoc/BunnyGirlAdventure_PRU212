using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerJump : MonoBehaviour
{
    public ParticleSystem dust;
    public float maxJumpForce = 20f;
    public float chargeRate = 20f;
    public float moveSpeed = 5f;
    public float knockbackForce = 10f; // The force of the knockback
    private float currentJumpForce;
    private bool isJumping = false;
    private bool isCharging = false;
    private Rigidbody2D rb;
    private Vector3 originalScale; // Store the original scale
    private bool facingRight = true;
    
    private Animator anim; // Animator component
    AudioSource jumpsound;

    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY"))
        {
            Vector2 playerPos = new Vector2(PlayerPrefs.GetFloat("PlayerPosX"), PlayerPrefs.GetFloat("PlayerPosY"));
            transform.position = playerPos;
        }
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // Get the original scale
        anim = GetComponent<Animator>(); // Get the Animator component
        jumpsound = GetComponent<AudioSource>();             // 
    }

    void Update()
    {
        // Start charging jump when button is pressed
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isCharging = true;
            currentJumpForce = 0f;
            rb.velocity = Vector2.zero; // Stop the character immediately
            anim.SetFloat("Speed", 0);

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
            dust.Play();
            rb.AddForce(new Vector2(0f, currentJumpForce), ForceMode2D.Impulse);
            isJumping = true;
            isCharging = false;
            jumpsound.Play();
        }

        // Horizontal Movement
        if (!isCharging) // Only allow movement if not charging jump
        {
            float moveX = Input.GetAxis("Horizontal");
            anim.SetFloat("Speed", Mathf.Abs(moveX));
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
            // Flip character based on direction
            //if (moveX > 0)
            //{
            //    dust.Play();
            //    transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            //    //dust.transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            //    // Face right
            //}
            //else if (moveX < 0)
            //{
            //    dust.Play();
            //    transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            //    //dust.transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);      
            //}
            if ((moveX > 0 && !facingRight) || (moveX < 0 && facingRight))
            {
                Flip();
            }
        }

        // Set the "Jump" parameter in the Animator
        anim.SetBool("Jump", isJumping);
    }

    private void Flip()
    {
        dust.Play();
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FinishPoint")
        {
            SceneManager.LoadScene(6);
        }
   
        if (collision.gameObject.tag == "BackScene")
        {
            // Save the desired position to PlayerPrefs
            PlayerPrefs.SetFloat("PlayerPosX", 9.16f);
            PlayerPrefs.SetFloat("PlayerPosY", 37.49f);
            SceneManager.LoadScene(1);
        }
        if (collision.gameObject.tag == "BackScene2")
        {
            // Save the desired position to PlayerPrefs
            PlayerPrefs.SetFloat("PlayerPosX", 56.47f);
            PlayerPrefs.SetFloat("PlayerPosY", 4.53f);
            SceneManager.LoadScene(2);
        }
        if (collision.gameObject.tag == "BackScene3")
        {
            // Save the desired position to PlayerPrefs
            PlayerPrefs.SetFloat("PlayerPosX", 43.08f);
            PlayerPrefs.SetFloat("PlayerPosY", 10.55f);
            SceneManager.LoadScene(3);
        }
        else if (collision.gameObject.tag == "EndScene1")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("UI"))
        {
            isJumping = false;
            // Determine the direction of the knockback
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            // Apply the knockback force horizontally
            rb.AddForce(new Vector2(knockbackDirection.x * knockbackForce, 0f), ForceMode2D.Impulse);
        }
        
    }
}
