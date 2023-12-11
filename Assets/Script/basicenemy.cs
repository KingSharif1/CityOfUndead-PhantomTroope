using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class basicenemy : MonoBehaviour
{
    // Point A need to be on the left side of the enemy and Point B on the right side of the enemy
    public GameObject PointA;
    public GameObject PointB;
    private Animator animator;
    private Rigidbody2D rb;
    private Transform currentpoint;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentpoint = PointB.transform;
        animator.SetBool("walking", true);

    }

    void Update()
    {
        Vector2 point = currentpoint.position - transform.position;
        
        Debug.Log($"Current enemy position: {currentpoint.position} || Enemy destination: {transform.position} = {point}");
        //walking
        if(currentpoint == PointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
            // animator.SetBool("walking", true);
        }else{

            rb.velocity = new Vector2(-speed, 0);
        }

        //change direction
        if(Vector2.Distance(transform.position, currentpoint.position) < 1.0f && currentpoint == PointB.transform)
        {
            flip();
            currentpoint = PointA.transform;
        }

        if(Vector2.Distance(transform.position, currentpoint.position) < 1.0f && currentpoint == PointA.transform)
        {
            flip();
            currentpoint = PointB.transform;
        }

    }

    private void flip()
    {
        Debug.Log("Should be Flip");
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }


//outling for border
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f); 
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f); 
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
    }

}