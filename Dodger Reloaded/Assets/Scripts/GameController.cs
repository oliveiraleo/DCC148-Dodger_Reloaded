using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshPro scoreText;
    private float score;
    private float scoreIncrementTimer = 0f; // timer for score increment, set to 0 to start
    private float topScore;
    public TextMeshPro topScoreText;
    public TextMeshPro gameOverText;
    public TextMeshPro playerFinalScoreText;
    public GameObject restartButton; // restartButton object
    public GameMenuController gameMenu;
    // Audio / Sound effects
    public AudioSource resetTopScoreSound;
    public AudioSource enemySpawnSound;
    public AudioSource playerDeathSound;
    private float deathSoundTimer = 0f; // timer for death sound
    public AudioSource newTopScoreSound;
    private bool newTopScoreSoundPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        //move the player to the center of the screen
        //player.transform.position = new Vector3(0, 0, 0);
        //spawnPlayer(); //instantiate the player
        //set all scores to 0 and update their text
        score = 0f;
        readScore();
        updateTopScore(topScore);
        numberOfEnemies = 0;
        updateScore();
        //enable the player
        player.gameObject.SetActive(true);
        //start the enemy spawn timer
        enemySpawnTimer = enemySpawnInterval;
        //add a listener to the restart button
        restartButton.GetComponent<Button>().onClick.AddListener(restartGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null){
            updateScore();
            //once the number of enemies on the screen reaches a certain number, reduce the spawn speed
            if (numberOfEnemies > 10) //'level' 1
            {
                enemySpawnInterval = 5f; // change the interval between enemy spawns to 5 seconds
            } else if (numberOfEnemies > 20) //'level' 2
            {
                enemySpawnInterval = 10f; // change the interval between enemy spawns to 10 seconds
            } else if (numberOfEnemies > 30) //'level' 3
            {
                enemySpawnInterval = 15f; // change the interval between enemy spawns to 15 seconds
            }
            //enemies spawn control
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
            if (deathSoundTimer >= -0.2 && deathSoundTimer < 0) // prevents multiple executions of the sound
            {
                playerDeathSound.Play();
            }
            //clear the score text
            scoreText.text = "Score: Game Over!";
            scoreText.color = Color.yellow;
            //update the top score
            updateTopScore(score);
            //enable the game over text
            gameOverText.gameObject.SetActive(true);
            //enable the final score text
            updateFinalScore();
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
        // adds a point every second
        scoreIncrementTimer += Time.deltaTime;
        if (scoreIncrementTimer >= 1f)
        {
            score += 1f;
            scoreIncrementTimer = 0f;
        }
        //if the score is greater than the top score, then change both to a new color
        if (score >= topScore && !newTopScoreSoundPlayed)
        {
            scoreText.color = Color.green;
            topScoreText.color = Color.red;
            newTopScoreSoundPlayed = true;
            newTopScoreSound.Play();
        }
        scoreText.text = "Score: " + (int)score;
    }

    void updateTopScore(float newScore)
    {
        if (newScore > topScore)
        {
            topScore = newScore;
            saveScore();
            //newTopScoreSound.Play();
        }
        topScoreText.text = "Top Score: " + (int)topScore;
    }

    void updateFinalScore()
    {
        playerFinalScoreText.gameObject.SetActive(true);
        playerFinalScoreText.text = ((int)score).ToString();
        if (score >= topScore)
        {
            playerFinalScoreText.color = Color.green;
        } else {
            playerFinalScoreText.color = Color.red;
        }
    }

    void spawnEnemy()
    {
        //create a new enemy object
        GameObject enemy = Instantiate(enemyPrefab);
        //set the enemy's position to a random position on the screen
        enemy.transform.position = getAnEnemyRandomPosition();
        //set the enemy's direction to a random direction
        enemy.GetComponent<EnemyController>().setARandomEnemyDirection();
        //play the enemy spawn sound
        enemySpawnSound.Play();
        //increment the enemy counter
        numberOfEnemies++;
        //prints the player's position and the new enemy's position on the console
        //Debug.Log("Player: " + (float)player.transform.position.x + " " + (float)player.transform.position.y);
        //Debug.Log("Enemy: " + (float)enemy.transform.position.x + " " + (float)enemy.transform.position.y);
    }

    Vector3 getAnEnemyRandomPosition()
    {
        Vector3 newPos = new Vector3(Random.Range(-horizontalLimits, horizontalLimits), Random.Range(-verticalLimits, verticalLimits), 0);
        //exclude the area near the player from the random position
        if (newPos.x >= (player.transform.position.x - 5.5) &&
        newPos.x <= (player.transform.position.x + 5.5) &&
        newPos.y >= (player.transform.position.y - 3.5) &&
        newPos.y <= (player.transform.position.y + 3.5))
        {
            return getAnEnemyRandomPosition();
        } else
        {
            return newPos;
        }
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
        //save the top score to the player prefs
        PlayerPrefs.SetInt("topScore", (int)topScore);
        //PlayerPrefs.SetInt("topScore", 0); // use this to reset the top score
    }

    void readScore()
    {
        //read the top score from the player prefs
        topScore = PlayerPrefs.GetInt("topScore");
    }
}
