using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = .05f; // speed of the enemy

    private float horizontalLimits = 8.5f; // horizontal boundaries of the screen

    private float verticalLimits = 4.6f; // horizontal boundaries of the screen

    private int enemyDirection; // direction of the enemy

    public GameObject enemy; // enemy object
    // Start is called before the first frame update
    void Start()
    {
        enemyDirection = getEnemyDirection();
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy movement on each axis
        switch (enemyDirection)
        {
            case 0:
                Vector3 position = this.transform.position;
                position.x -= speed;
                this.transform.position = position;
                break;
            case 1:
                Vector3 position1 = this.transform.position;
                position1.x += speed;
                this.transform.position = position1;
                break;
            case 2:
                Vector3 position2 = this.transform.position;
                position2.y += speed;
                this.transform.position = position2;
                break;
            case 3:
                Vector3 position3 = this.transform.position;
                position3.y -= speed;
                this.transform.position = position3;
                break;
        }
    }

    int getRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }

    int getEnemyDirection()
    {
        int direction = getRandomNumber(0, 4);
        return direction;
    }
}
