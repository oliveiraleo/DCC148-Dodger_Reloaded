using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = .1f; // speed of the player
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        #region Keyboard Input
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
        #endregion
    }
}
