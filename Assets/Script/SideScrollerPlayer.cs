using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerPlayer : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    public float jumpForce = 500.0f;

    Rigidbody2D rb;

    public bool isGrounded = false;

    public bool shouldJump = false;

    Animator animator;

    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // get horizontal n vertical input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


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
  
        //running
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

        //jumping
        if (verticalInput > 0f || Input.GetButtonDown("Jump") && isGrounded )
        {
            // Up button is pressed
            Debug.Log("Up pressed");
            shouldJump = true;

        }
        else if (verticalInput < 0f)
        {
            // Down button is pressed
            Debug.Log("Down pressed");
        }

    
    }

    void FixedUpdate()
    {
        if (isGrounded && shouldJump == true)
        {
            // quickly set back to false so we don't double-jump
            shouldJump = false;

            //push the rigidbody UP
            rb.AddForce(transform.up * jumpForce);

            //animate
            animator.SetBool("isJumping", true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isGrounded = true;
        //rest animate
        animator.SetBool("isJumping", false);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
    }
}
