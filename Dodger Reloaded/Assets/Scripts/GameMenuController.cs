using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    public GameObject quitButton; // quitButton object
    public GameObject restartButton; // restartButton object
    private float restartTimer = 3f; // interval until the game menu is enabled
    // Start is called before the first frame update
    void Start()
    {
        //force disable the end game menu buttons
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        quitButton.GetComponent<Button>().onClick.AddListener(quitGame); // add a listener to the quit button
    }

    // Update is called once per frame
    void Update()
    {

    }

    void quitGame()
    {
        //Debug.Log("You have clicked the quit button!");
        Application.Quit();
    }

    public void loadMenuEndGame()
    {
        restartTimer -= Time.deltaTime;
        if (restartTimer <= 0)
        {
            restartButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
        }
    }

    public void unloadMenuEndGame()
    {
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }
}
