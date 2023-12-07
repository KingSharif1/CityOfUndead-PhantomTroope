using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPlayer : MonoBehaviour
{
    GameManager gameManager;
    public float moveSpeed = 10.0f;

    public float jumpForce = 20.0f;

    Rigidbody2D rb;

    public bool isGrounded = false;

    public bool shouldJump = false;
    private bool doublejmp = false;

    Animator animator;

    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("Canvas").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // get horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");
        if(isGrounded || !Input.GetButtonDown("Jump") || !Input.GetButtonDown("Vertical"))
        {
            doublejmp = false;
        }

        if (isGrounded)
        {
            transform.Translate(new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime);
            
        }
        else
        {
            moveSpeed = 7;
            transform.Translate(new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime);
            moveSpeed = 10;
        }

        //animate
        if (horizontalInput > 0)
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Vertical") && isGrounded)
        {
            shouldJump = true;
        }
        else 
        if(Input.GetButtonDown("Jump") || Input.GetButtonDown("Vertical"))
        {
            if(isGrounded || doublejmp)
            {
                shouldJump = true;
                doublejmp = !doublejmp;
            }
        }
    }

    void FixedUpdate()
    {
        if (isGrounded && shouldJump == true)
        {
            // quickly set back to false so we don't double-jump
            shouldJump = false;

            //push the rigidbody UP
            // rb.AddForce(transform.up * jumpForce);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            //animate
            animator.SetBool("isJumping", true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isGrounded = true;
        //rest animate
        animator.SetBool("isJumping", false);
        if (gameManager.numOfLives >= 0 && other.CompareTag("Enemy")) // Assuming enemy collision
        {
            // = true;  Set isHit to true when hit by an enemy
            animator.SetTrigger("isHit"); // Trigger the isHit animation
        }else if(gameManager.numOfLives < 0)
        {
            animator.SetTrigger("Dead");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
    }
}
