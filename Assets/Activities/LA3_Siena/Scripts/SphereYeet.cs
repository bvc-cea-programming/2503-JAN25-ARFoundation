using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SphereYeet : MonoBehaviour
{
    [SerializeField] 
    private float launchForce = 15f; // Determines how much force is applied when launching

    [SerializeField] 
    private float upwardForce = 0.5f;  //Additional upward force applied

    [SerializeField] 
    private float minimumThrowDistance = 30f;  //Minimum throw distance to count action as a "throw"

    private Vector2 startPosition; //Start position of mouse click / touch
    private Vector2 endPosition; //End position of mouse click / touch
    private Vector2 throwDirection; //Direction of the ball throw

    private Vector3 cameraForward;
    private Vector3 launchDirection;

    private Rigidbody sphereRigidbody;

    private bool hasBeenLaunched = false; //Ensures the object can only be launched once

    void Start()
    {
        sphereRigidbody = GetComponent<Rigidbody>();
        sphereRigidbody.useGravity = false; // Disable gravity initially so the object doesn't fall before launch
    }

    void Update()
    {
        HandleSwipe(); // Calls the method to detect swipe input
    }

    private void HandleSwipe()
    {
        if (hasBeenLaunched) // If the object has already been launched, don't process input
            return;

        /*
        // Detect touch input on mobile devices
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch input

            if (touch.phase == TouchPhase.Began) // When the touch starts
                startPosition = touch.position; // Store the starting position

            else if (touch.phase == TouchPhase.Ended) // When the touch ends
            {
                endPosition = touch.position; // Store the ending position
                LaunchBall(); 
            }
        }
        */

        // Detect mouse input for PC users
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
            startPosition = Input.mousePosition; // Store the starting position

        else if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            endPosition = Input.mousePosition; // Store the ending position
            LaunchBall(); 
        }
    }

    private void LaunchBall()
    {
        throwDirection = endPosition - startPosition;

        if (throwDirection.y > minimumThrowDistance)  // Check if the swipe was mostly in the upward direction
        {
            sphereRigidbody.useGravity = true; // Enable gravity so the object falls naturally
            hasBeenLaunched = true; // Mark the object as launched to prevent re-launching

            cameraForward = Camera.main.transform.forward; // Get the forward direction of the camera
            cameraForward.y = 0; // Ignore vertical component to ensure horizontal movement

            // Create a launch direction based on the camera's forward direction with a slight upward boost
            launchDirection = cameraForward.normalized + Vector3.up * upwardForce;

            // Apply an impulse force in the calculated direction to launch the object
            sphereRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Monster"))
        {
            Destroy(collider.gameObject);
            Destroy(this.gameObject);
        }
    }
}
