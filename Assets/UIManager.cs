using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private int playerhealth = 5;
    [SerializeField] private Text healthDisplay = null;
    private int goldScore = 0;
    [SerializeField] private Text goldDisplay = null;

    //[SerializeField] private GameObject TitleScreen = null;

    // Start is called before the first frame update
    void Start()
    {
        //highScore = PlayerPrefs.GetInt("HighScoore", 0); // 0 is default

    }

    public void UpdateHealth(int health)
    {
        playerhealth = health;
        healthDisplay.text = "Health: " + playerhealth;
    }

    public void UpdateScore(int pickup)
    {
        goldScore += pickup;
        goldDisplay.text = "Gold: " + goldScore;

        //CheckForHighScore();
    }
}
