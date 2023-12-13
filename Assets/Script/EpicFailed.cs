using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EpicFailed : MonoBehaviour
{
    GameManager gameManager;
    private bool isColliding = false;
    private Animator animator;

    private void Start()
    {
        gameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
        animator = GetComponent<Animator>();

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isColliding) return;


        if(other.gameObject.tag == "Player")
        {
            isColliding = true;
            Debug.Log("Triggering attack animation");
            this.animator.SetTrigger("attack");

            if(gameManager.numOfLives == 0)
            {
                Animator playerAnimator = other.gameObject.GetComponent<Animator>();
                playerAnimator.SetTrigger("Dead");

                StartCoroutine(ReloadSceneAfterDeath(playerAnimator));
            }
            else if(gameManager.numOfLives > 0)
            {

                gameManager.numOfLives--;
                other.gameObject.GetComponent<Animator>().SetTrigger("isHit");
                Debug.Log("ik that hurt");
            }
            StartCoroutine(Reset());

        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2);
        isColliding = false;
    }

    IEnumerator ReloadSceneAfterDeath(Animator playerAnimator)
    {
        yield return new WaitForSeconds(1); // Adjust the delay time as needed

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("GGs buddy");
    }

}

