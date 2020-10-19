using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //private variables
    public float horseEngine = 100.0f;
    [SerializeField] private float speed;
    [SerializeField] private GameObject centreOfMass;
    [SerializeField] TextMeshProUGUI speedMetre;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;

    private float turnSpeed = 25.0f;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody playerRb;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centreOfMass.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //where we get the input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Move the vehicle forward
        //  transform.Translate(Vector3.forward*Time.deltaTime*speed*verticalInput);
        playerRb.AddRelativeForce(Vector3.forward * horseEngine * verticalInput);
        //Rotate the vehicle 
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        if (IsOnGround())
        {
            speed = Mathf.Round(playerRb.velocity.magnitude * 2.237f);
            speedMetre.SetText("Speed: " + speed + "mph");
        }
    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if(wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }
        if (wheelsOnGround == 4)
            return true;
        else
            return false;
    }
}
