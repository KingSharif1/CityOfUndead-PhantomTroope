using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpKey : MonoBehaviour
{
    GameManager gameManager;
    private bool didCountKey = false;

    private void Start()
    {
        gameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !didCountKey)
        {
            didCountKey = true;
            gameManager.numOfKeys++;
            gameManager.audioSource.Play();
            Destroy(gameObject);
        }
    }
}
