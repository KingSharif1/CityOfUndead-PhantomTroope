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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Debug.Log("GGs buddy");
            }
            else
            {
                gameManager.numOfLives--;
                other.gameObject.transform.position = gameManager.spawnPoint;
                Debug.Log("nice try buddy");
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

