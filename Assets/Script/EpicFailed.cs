using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EpicFailed : MonoBehaviour
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


        if(other.gameObject.tag == "Player")
        {
            isColliding = true;
            if(gameManager.numOfLives == 0)
            {
                other.gameObject.GetComponent<Animator>().SetTrigger("Dead");

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                // other.gameObject.transform.position = gameManager.spawnPoint;

                Debug.Log("GGs buddy");
            }
            else if(gameManager.numOfLives > 0)
            {
                gameManager.numOfLives--;
                Debug.Log("ik that hurt");
            }
            StartCoroutine(Reset());

        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
        isColliding = false;
    }

}

