using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class basicenemy : MonoBehaviour
{
    // Point A need to be on the left side of the enemy and Point B on the right side of the enemy
    public GameObject PointA;
    public GameObject PointB;
    public float speed;
    public float attackRange = 5f;
    // public LayerMask playerLayer;

    private Animator animator;
    private Rigidbody2D rb;
    private Transform currentpoint;
    private Transform player;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentpoint = PointB.transform;
        animator.SetBool("walking", true);

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

    //walk back and froth from pointA and pontB
    void Move()
    {
        Vector2 point = currentpoint.position - transform.position;
        
        Debug.Log($"Current enemy position: {currentpoint.position} || Enemy destination: {transform.position} = {point}");
        //walking
        if (currentpoint == PointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        //change direction
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

    //check if player in rang
    void CheckPlayerInRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // Attack the player
            Debug.Log("Attacking Player!");
            isAttacking = true;

            // Implement your attack logic here
        }
        else
        {
            isAttacking = false;
        }
    }

    //flip after touch end of one point
    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // Gizmos for visualizing points
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}