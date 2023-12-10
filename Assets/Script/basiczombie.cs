using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basiczombie : MonoBehaviour
{

    public bool goLeft = true;
    public float mvSpeed = 2.0f;
    public float attackRange = 3.0f; // Distance at which the monster attacks
    public float attackCooldown = 2.0f; // Cooldown between attacks
    private float direction = -1.0f;
    private Vector3 startingScale;
    private Transform player;
    private bool canAttack = true;

    Animator animator;
    SpriteRenderer spriteRenderer;


    void Start()
    {
        startingScale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if the player is within attack range
        if (distanceToPlayer <= attackRange && canAttack)
        {
            // Attack the player (you can add your attack logic here)
            AttackPlayer();
        }
        else
        {
            // Move back and forth in a location
            MoveBackAndForth();
        }
    }

    void MoveBackAndForth()
    {
        if (goLeft)
        {
            direction = -1.0f;
            transform.localScale = startingScale;

            animator.SetBool("walking", true);
            spriteRenderer.flipX = false;

        }
        else
        {
            direction = 1.0f;
            transform.localScale = new Vector3(-startingScale.x, startingScale.y, startingScale.z);

            animator.SetBool("walking", true);
            spriteRenderer.flipX = true;        
        }
        transform.Translate(new Vector3(direction, 0, 0) * mvSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        // Implement your attack logic here
        Debug.Log("Attacking player!");
        
        // For example, you can stop moving while attacking
        canAttack = false;

        // Perform attack animation or action
        animator.SetTrigger("attack");
        
        // After attacking, start cooldown before the monster can attack again
        Invoke("ResetAttack", attackCooldown);
    }

    void ResetAttack()
    {
        // Reset attack cooldown and resume movement
        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Change direction when colliding with a wall or obstacle
        if (other.gameObject.CompareTag("Wall"))
        {
            goLeft = !goLeft;
        }
    }

  
}
