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

        // Change direction based on player position
        // if (player != null)
        // {
            
        // }

        // Change point when reaching destination
        if (Vector2.Distance(transform.position, currentpoint.position) < 1.0f && currentpoint == PointB.transform)
        {
            Flip();
            currentpoint = PointA.transform;
        }

        if (Vector2.Distance(transform.position, currentpoint.position) < 1.0f && currentpoint == PointA.transform)
        {
            Flip();
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
                FlipToPlayerDirection(Vector3.right);
                // FlipToPlayerDirection(Vector3.left);
            }
            else if(transform.position.x > player.position.x && isAttacking == true)
            {
                Debug.Log("Flip to left to player");
                FlipToPlayerDirection(Vector3.left);
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
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if the zombie is not already at the original position
        if (distanceToOriginalPosition > 0f)
        {
            // Debug.Log("Moving back to original position");
            //flip the zombie to face the original position
            if (Vector2.Distance(transform.position, currentpoint.position) < 1.0f && currentpoint == PointB.transform)
            {
                Debug.Log("Flip PointB(right)");
                Vector3 localScale = transform.localScale;
                // localScale.x = direction.x;
                localScale.x = 1.0f;
                transform.localScale = localScale;

                // Flip();
                currentpoint = PointA.transform;
            }
            if (Vector2.Distance(transform.position, currentpoint.position) < 1.0f && currentpoint == PointA.transform)
            {
                Debug.Log("Flip PointA(left)");
                Vector3 localScale = transform.localScale;
                // localScale.x = direction.x;
                localScale.x = -1.0f;
                transform.localScale = localScale;
                // Flip();
                currentpoint = PointB.transform;
            }
   
            // Move towards the original position
            transform.position = Vector2.MoveTowards(transform.position, currentpoint.position, speed * Time.deltaTime);
        }
    }

    void FlipToPlayerDirection(Vector3 direction)
    {

        Debug.Log($"Direction: {direction} | localScale: {transform.localScale} | localScale.X: {transform.localScale.x}");
        // Vector3 localScale = transform.localScale;
        if(direction.x == 1.0f)
        {
            Debug.Log("Flip to right ");
            Vector3 localScale = transform.localScale;
            localScale.x = 0.5f;
            transform.localScale = localScale;

            Debug.Log("movig to player");
            transform.position = Vector2.MoveTowards(this.transform.position, Playergo.transform.position, speed * Time.deltaTime);
        }
        else if(direction.x == -1.0f)
        {
            Debug.Log("Flip to left");
            Vector3 localScale = transform.localScale;
            localScale.x = -0.5f;
            transform.localScale = localScale;  

            Debug.Log("movig to player");
            transform.position = Vector2.MoveTowards(this.transform.position, Playergo.transform.position, speed * Time.deltaTime);
        }

    }

    void Flip()
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


    //killing player  


}
