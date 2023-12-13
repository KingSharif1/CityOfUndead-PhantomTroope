using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class walldamange : MonoBehaviour
{
    GameManager gameManager;
    private bool isColliding = false;

    private void Start()
    {
        gameManager = GameObject.Find("Canvas").GetComponent<GameManager>();

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isColliding) return;


        if (other.gameObject.tag == "Player")
        {
            isColliding = true;

            Animator playerAnimator = other.gameObject.GetComponent<Animator>();
            playerAnimator.SetTrigger("Dead");
            StartCoroutine(ReloadSceneAfterDeath(playerAnimator));

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
