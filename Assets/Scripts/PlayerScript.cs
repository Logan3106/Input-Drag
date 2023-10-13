using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;

    public Transform orientation;

    public Vector3 moveDirect;

    public Joystick joystick;

    public float movingSpeed;

    public float xInput;
    public float yInput;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovingPlayer();
    }

    public void MovingPlayer()
    {
        xInput = joystick.Horizontal;
        yInput = joystick.Vertical;

        moveDirect = orientation.forward * yInput + orientation.right * xInput;
        rigidbody2D.AddForce(moveDirect.normalized * movingSpeed * 10f);
    }
}
