using UnityEngine;

public class CopyBall : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private Rigidbody rb;

    [SerializeField] private float launchForce = 15f;  
    [SerializeField] private float upwardForce = 7f;   
    [SerializeField] private float gravityModifier = 0.5f;  
    private bool hasBeenLaunched = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; 
    }

    void Update()
    {
        HandleSwipe();
    }

    private void HandleSwipe()
    {
        if (hasBeenLaunched) return; 

        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                LaunchBall();
            }
        }

        
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;
            LaunchBall();
        }
    }

    private void LaunchBall()
    {
        Vector2 swipeDirection = endTouchPosition - startTouchPosition;

        if (swipeDirection.y > 50) 
        {
            rb.useGravity = true;
            hasBeenLaunched = true;

           
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0; 

            Vector3 launchDirection = cameraForward.normalized + Vector3.up * 0.5f; 
            rb.AddForce(launchDirection * launchForce, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Copymon")) 
        {
            Destroy(other.gameObject); 
            Destroy(gameObject); 
        }
    }
}
