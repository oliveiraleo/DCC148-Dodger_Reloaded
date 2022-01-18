using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //public GameObject enemy; // enemy object

    public GameObject player; // player object
    public Text scoreText;
    private int score;

    private int topScore;
    public Text topScoreText;

    public Text gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        //move the player to the center of the screen
        player.transform.position = new Vector3(0, 0, 0);
        //set all scores to 0 and update their text
        score = 0;
        topScore = 0;
        updateTopScore();
        updateScore();
        //enable the player
        player.gameObject.SetActive(true);
        //disable the game over text
        gameOverText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null){
            updateScore();
        } else {
            //if the player is null, then the game is over
            //update the score text
            scoreText.text = "Score: Game Over!";
            //update the top score text
            updateTopScore();
            topScoreText.text = "Top Score: " + topScore;
            gameOverText.text = "You are dead!\nGame Over!";
        }
        
    }

    void updateScore()
    {
        score += 1;
        scoreText.text = "Score: " + score;
    }

    void updateTopScore()
    {
        topScore = score;
        topScoreText.text = "Top Score: " + topScore;
    }
}
