using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int maxMovement = 7; // max movement options of the enemy
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
        //Enemy movement control
        if (this.transform.position.x < -horizontalLimits)
        {
            this.transform.position = new Vector3(-horizontalLimits, this.transform.position.y, 0);
            enemyDirection = getOtherEnemyDirection(this.enemyDirection);
        }
        else if (this.transform.position.x > horizontalLimits)
        {
            this.transform.position = new Vector3(horizontalLimits, this.transform.position.y, 0);
            enemyDirection = getOtherEnemyDirection(this.enemyDirection);
        }
        //Vertical movement control
        else if (this.transform.position.y < -verticalLimits)
        {
            this.transform.position = new Vector3(this.transform.position.x, -verticalLimits, 0);
            enemyDirection = getOtherEnemyDirection(this.enemyDirection);
        }
        else if (this.transform.position.y > verticalLimits)
        {
            this.transform.position = new Vector3(this.transform.position.x, verticalLimits, 0);
            enemyDirection = getOtherEnemyDirection(this.enemyDirection);
        }
        else switch (enemyDirection)
            {
                //Enemy movement on each axis
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
                case 4:
                    Vector3 position4 = this.transform.position;
                    position4.x -= speed;
                    this.transform.position = position4;
                    Vector3 position4b = this.transform.position;
                    position4b.y -= speed;
                    this.transform.position = position4b;
                    break;
                case 5:
                    Vector3 position5 = this.transform.position;
                    position5.x += speed;
                    this.transform.position = position5;
                    Vector3 position5b = this.transform.position;
                    position5b.y -= speed;
                    this.transform.position = position5b;
                    break;
                case 6:
                    Vector3 position6 = this.transform.position;
                    position6.x += speed;
                    this.transform.position = position6;
                    Vector3 position6b = this.transform.position;
                    position6b.y += speed;
                    this.transform.position = position6b;
                    break;
                case 7:
                    Vector3 position7 = this.transform.position;
                    position7.x -= speed;
                    this.transform.position = position7;
                    Vector3 position7b = this.transform.position;
                    position7b.y += speed;
                    this.transform.position = position7b;
                    break;
            }
    }

    int getRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }

    int getEnemyDirection()
    {
        int direction = getRandomNumber(0, maxMovement);
        return direction;
    }

    int getOtherEnemyDirection(int direction)
    {
        int otherDirection = getRandomNumber(0, maxMovement);
        while (otherDirection == direction)
        {
            otherDirection = getRandomNumber(0, maxMovement);
        }
        return otherDirection;
    }
}