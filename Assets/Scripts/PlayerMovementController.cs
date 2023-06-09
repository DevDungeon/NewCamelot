using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    
    public CharacterController characterController;
    public Camera playerCamera;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private float speedBoost;
    private float mouseXSensitivity = 2;
    private float mouseYSensitivity = 1;
    private float verticalCameraRotationTracker = 0;
    private float verticalCameraRotationMinimum = -25; // from starting point
    private float verticalCameraRotationMax = 60; // from starting point

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) {
            speedBoost = 1.5f;
        } else {
            speedBoost = 1.0f;
        }
        //characterController.Move(new Vector3(Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime * speedBoost, 0, Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime * speedBoost));
        Vector3 forwardMovement = -transform.forward * Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime * speedBoost;
        Vector3 sideMovement = -transform.right * Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime * speedBoost;
        characterController.Move(forwardMovement + sideMovement);


        /**
         * Left mouse click
         */
        if (Input.GetMouseButton(0)) {

        }

        /**
         * Right mouse click
         */
        if (Input.GetMouseButton(1)) {
            /**
             * Horizontal player rotation
             */
            Cursor.lockState = CursorLockMode.Locked; // CursorLockMode.Locked    CursorLockMode.None
            transform.Rotate(0, Input.GetAxis("Mouse X") * mouseXSensitivity, 0); // Rotate horizontally and move the entire character

            /**
             * Vertical camera rotation
             */
            verticalCameraRotationTracker -= Input.GetAxis("Mouse Y"); // Update first and then check if it's within bounds
            // Rotate camera vertically around the player (ONLY if they are within the range)
            if (verticalCameraRotationTracker < verticalCameraRotationMax && verticalCameraRotationTracker > verticalCameraRotationMinimum) {
                // Debug.Log("Changing verticalCameraRotationTracker by " + Input.GetAxis("Mouse Y"));
                // verticalCameraRotationTracker += Input.GetAxis("Mouse Y");
                playerCamera.transform.RotateAround(transform.position, playerCamera.transform.right, -Input.GetAxis("Mouse Y") * mouseYSensitivity);
            } else if (verticalCameraRotationTracker >= verticalCameraRotationMax) { // Higher is closer to ground
                verticalCameraRotationTracker = verticalCameraRotationMax;
            } else if (verticalCameraRotationTracker <= verticalCameraRotationMinimum) { // Lower is higher in the air
                verticalCameraRotationTracker = verticalCameraRotationMinimum;
            }
            // Debug.Log("New verticalCameraRotationTracker is " + verticalCameraRotationTracker);
    //            playerCamera.transform.rotation = Quaternion.Euler(0.4f, playerCamera.transform.rotation.y, playerCamera.transform.rotation.z);
            // }
        } else {
            Cursor.lockState = CursorLockMode.None;
        }

        
        // Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // characterController.Move(move * Time.deltaTime * playerSpeed);

        // if (move != Vector3.zero)
        // {
        //     gameObject.transform.forward = move;
        // }


        /**
         * Jump
         */
        groundedPlayer = characterController.isGrounded;
        Debug.Log("Grounded?" + groundedPlayer + " Player veloc? " + playerVelocity.y);
        
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Debug.Log("Zerod veloc"  + playerVelocity.y);
        
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);


    }
}
