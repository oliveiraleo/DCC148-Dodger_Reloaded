using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = .1f; // speed of the player

    private float horizontalLimits = 8.5f; // horizontal boundaries of the screen

    private float verticalLimits = 4.6f; // horizontal boundaries of the screen

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        #region Keyboard Input
        //Horizontal movement control
        if (this.transform.position.x < -horizontalLimits)
        {
            this.transform.position = new Vector3(-horizontalLimits, this.transform.position.y, 0);
        }
        else if (this.transform.position.x > horizontalLimits)
        {
            this.transform.position = new Vector3(horizontalLimits, this.transform.position.y, 0);
        }
        //Vertical movement control
        if (this.transform.position.y < -verticalLimits)
        {
            this.transform.position = new Vector3(this.transform.position.x, -verticalLimits, 0);
        }
        else if (this.transform.position.y > verticalLimits)
        {
            this.transform.position = new Vector3(this.transform.position.x, verticalLimits, 0);
        }
        else
        {
            //Player movement on each axis
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Vector3 position = this.transform.position;
                position.x -= speed;
                this.transform.position = position;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Vector3 position = this.transform.position;
                position.x += speed;
                this.transform.position = position;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Vector3 position = this.transform.position;
                position.y += speed;
                this.transform.position = position;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Vector3 position = this.transform.position;
                position.y -= speed;
                this.transform.position = position;
            }
        }
        #endregion
    }
}
