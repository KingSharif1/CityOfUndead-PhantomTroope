using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Bullet")
        {
            //destroy other
            Destroy(other.gameObject);

            if(animator != null)
            {
                //animate
                animator.SetTrigger("gotKilled");
                Destroy(gameObject, 0.5f);

            }
            else
            {
                //unalive our self
                Destroy(gameObject);
            }


        }
    }
}
