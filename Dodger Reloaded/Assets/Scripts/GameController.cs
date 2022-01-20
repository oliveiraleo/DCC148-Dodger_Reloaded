using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Visual elements / Game objects
    public GameObject enemyPrefab; // enemy object
    public GameObject playerPrefab; // enemy object
    private float enemySpawnInterval = 1f; // interval between enemy spawns. in seconds
    private float enemySpawnTimer; // timer for enemy spawns
    private float horizontalLimits = 8.5f; // horizontal boundaries of the screen
    private float verticalLimits = 4.6f; // horizontal boundaries of the screen
    private int numberOfEnemies; // number of enemies on the screen
    public GameObject player; // player object
    // UI elements
    public Text scoreText;
    private float score;
    private float topScore;
    public Text topScoreText;
    public Text gameOverText;
    public Text playerFinalScoreText;
    public GameObject restartButton; // restartButton object
    public GameMenuController gameMenu;
    // Audio / Sound effects
    public AudioSource resetTopScoreSound;
    public AudioSource enemySpawnSound;
    public AudioSource playerDeathSound;
    private float deathSoundTimer = 0f; // timer for death sound
    public AudioSource newTopScoreSound;

    // Start is called before the first frame update
    void Start()
    {
        //move the player to the center of the screen
        //player.transform.position = new Vector3(0, 0, 0);
        //spawnPlayer(); //instantiate the player
        //set all scores to 0 and update their text
        score = 0f;
        //topScore = 0;
        readScore();
        updateTopScore(topScore);
        numberOfEnemies = 0;
        //PlayerPrefs.DeleteKey("topScore");
        updateScore();
        //enable the player
        player.gameObject.SetActive(true);
        //disable the game over text
        gameOverText.gameObject.SetActive(false);
        //restartButton.gameObject.SetActive(false); // not needed anymore because we are using the GameMenuController
        //start the enemy spawn timer
        enemySpawnTimer = enemySpawnInterval;
        restartButton.GetComponent<Button>().onClick.AddListener(restartGame); // add a listener to the restart button
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null){
            updateScore();
            //once the number of enemies on the screen reaches a certain number, reduce the spawn speed
            if (numberOfEnemies > 15)
            {
                enemySpawnInterval = 5f; // change the interval between enemy spawns to 5 seconds
            } else if (numberOfEnemies > 30)
            {
                enemySpawnInterval = 10f; // change the interval between enemy spawns to 5 seconds
            }
            enemySpawnTimer -= Time.deltaTime; // starts the counter for enemy spawns
            if (enemySpawnTimer <= 0 && numberOfEnemies <= 50)
            {
                enemySpawnTimer = enemySpawnInterval; // reset the enemy spawn timer
                spawnEnemy(); // spawn an enemy
            }
        } else {
            //if the player is null, then the game is over
            //play the player death sound
            deathSoundTimer -= Time.deltaTime;
            if (deathSoundTimer >= -0.2 && deathSoundTimer < 0) // prevents muiltiple excutions of the sound
            {
                playerDeathSound.Play();
            }
            //clear the score text
            scoreText.text = "Score: Game Over!";
            //update the top score
            updateTopScore(score);
            //enable the game over text
            gameOverText.gameObject.SetActive(true);
            //enable the final score text
            playerFinalScoreText.text = ((int)score).ToString();
            playerFinalScoreText.gameObject.SetActive(true);
            //loads the game menu
            gameMenu.loadMenuEndGame();
            saveScore();
        }
        //enables a key to clear the top score
        if (Input.GetKeyDown(KeyCode.Space)){
            PlayerPrefs.SetInt("topScore", 0);
            readScore();
            updateTopScore(topScore);
            resetTopScoreSound.Play();
        }
    }

    void updateScore()
    {
        score += 1/30f; // adds 2 points every second
        scoreText.text = "Score: " + (int)score;
    }

    void updateTopScore(float newScore)
    {
        if (newScore > topScore)
        {
            topScore = newScore;
            saveScore();
            newTopScoreSound.Play();
        }
        topScoreText.text = "Top Score: " + (int)topScore;
    }

    void spawnEnemy()
    {
        //create a new enemy object
        GameObject enemy = Instantiate(enemyPrefab);
        //set the enemy's position to a random position on the screen
        enemy.transform.position = new Vector3(Random.Range(-horizontalLimits, horizontalLimits), Random.Range(-verticalLimits, verticalLimits), 0);
        //set the enemy's direction to a random direction
        enemy.GetComponent<EnemyController>().setARandomEnemyDirection();
        //play the enemy spawn sound
        enemySpawnSound.Play();
        //increment the enemy counter
        numberOfEnemies++;
    }

    void spawnPlayer()
    {
        //create a new enemy object
        GameObject player = Instantiate(playerPrefab);
        //move the player to the center of the screen
        player.transform.position = new Vector3(0, 0, 0);
    }

    public void restartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    void saveScore()
    {
        //save the score to the player prefs
        //PlayerPrefs.SetInt("score", score);
        //save the top score to the player prefs
        PlayerPrefs.SetInt("topScore", (int)topScore);
        //PlayerPrefs.SetInt("topScore", 0); // use it to reset the top score
    }

    void readScore()
    {
        //read the score from the player prefs
        //score = PlayerPrefs.GetInt("score");
        //read the top score from the player prefs
        topScore = PlayerPrefs.GetInt("topScore");
    }
}
