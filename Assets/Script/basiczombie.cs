using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class basiczombie : MonoBehaviour
{

    GameManager gameManager;
    public GameObject PointA;
    public GameObject PointB;
    public float speed;
    public float attackRange = 4f;
    // public LayerMask playerLayer;

    private Animator animator;
    private Rigidbody2D rb;
    private Transform currentpoint;
    private Transform player;
    private bool isAttacking = false;
    private GameObject Playergo;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentpoint = PointB.transform;
        animator.SetTrigger("walking");

        Playergo = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming Player has a "Player" tag
    }

    void Update()
    {
        if (!isAttacking)
        {
            Move();
        }
        else
        {
            FlipToPlayerDirection();
        }
        CheckPlayerInRange();
    }

    void Move()
    {
        if (currentpoint == PointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        // Change point when reaching destination
        if (Vector2.Distance(transform.position, currentpoint.position) < 1.0f && currentpoint == PointB.transform)
        {
            AutoFlip();
            currentpoint = PointA.transform;
        }

        if (Vector2.Distance(transform.position, currentpoint.position) < 1.0f && currentpoint == PointA.transform)
        {
            AutoFlip();
            currentpoint = PointB.transform;
        }
    }

    void CheckPlayerInRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            isAttacking = true;

            // Attack the player
            // Debug.Log($"(In)Player found: {transform.position.x < player.position.x}");
            if (transform.position.x < player.position.x && isAttacking == true)
            {
                Debug.Log("Flip to right to player");
                FlipToPlayerDirection();
                // FlipToPlayerDirection(Vector3.left);
            }
            else if(transform.position.x > player.position.x && isAttacking == true)
            {
                Debug.Log("Flip to left to player");
                FlipToPlayerDirection();
            }
            // Attack();
            // Implement your attack logic here
        }
        else
        {
            isAttacking = false;
        // Move back to the original position
            MoveBackToOriginalPosition();

        }

        isAttacking = false;
    }

    void MoveBackToOriginalPosition()
    {
        // Calculate distance to the original position
        float distanceToOriginalPosition = Vector2.Distance(transform.position, currentpoint.position);

        // Check if the zombie is not already at the original position
        if (distanceToOriginalPosition > 0f)
        {
        // Update facing direction based on movement
            if (rb.velocity.x > 0)
            {
                // If moving right
                if (transform.localScale.x < 0)
                {
                    Flip(Vector3.right);

                    // Vector3 localScale = transform.localScale;
                    // localScale.x *= -1f; // Flip the sprite
                    // transform.localScale = localScale;
                }
            }
            else if (rb.velocity.x < 0)
            {
                // If moving left
                if (transform.localScale.x > 0)
                {
                    Flip(Vector3.left);
                    // Vector3 localScale = transform.localScale;
                    // localScale.x *= -1f; // Flip the sprite
                    // transform.localScale = localScale;
                }
            }
            // Move towards the original position
            transform.position = Vector2.MoveTowards(transform.position, currentpoint.position, speed * Time.deltaTime);
        }
    }

    void FlipToPlayerDirection()
    {

        if (player != null)
        {
            // Get player's Rigidbody2D component
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();

            // Predict the player's future position based on its velocity
            Vector2 predictedPlayerPos = player.position + (Vector3)playerRb.velocity * Time.deltaTime;

            // Move zombie towards the predicted position
            transform.position = Vector2.MoveTowards(transform.position, predictedPlayerPos, speed * Time.deltaTime);

            // Flip towards player's direction
            if (transform.position.x < player.position.x)
            {
                Flip(Vector3.right);
            }
            else
            {
                Flip(Vector3.left);
            }
        }

    }

    void Flip(Vector3 direction)
    {
        Vector3 localScale = transform.localScale;
        localScale.x = direction.x;
        transform.localScale = localScale;
    }

    void AutoFlip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    void Attack()
    {

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            if(distance <= 1.5f)
            {
                animator.SetTrigger("attack");
                Debug.Log("Attack");
            }

            Debug.Log($"Attacking player {distance}");

        }
        

    }

    // Gizmos for visualizing points
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

  

}
