using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int numOfLives = 3;
    public int numOfKeys = 0;
    public int numOfCoins = 0; 

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI keysText;
    public TextMeshProUGUI coinsText;

    public Vector3 spawnPoint;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector3(0, 0, 0);
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives: "+numOfLives;
        keysText.text = "Keys: "+numOfKeys;
        coinsText.text = "Coins: "+numOfCoins;

        if(numOfCoins == 5)
        {
            numOfCoins = 0;
            numOfLives++;
        }
    }
}
