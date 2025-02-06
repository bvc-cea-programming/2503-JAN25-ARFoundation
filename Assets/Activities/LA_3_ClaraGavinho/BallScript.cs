using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 direction;

    private float startTime;
    private float endTime;
    private float timeDiff;

    [SerializeField] private float throwForce = 15f;
    [SerializeField] private float throwForceZ = 7f;
    //[SerializeField] private float gravity = 0.5f;
    
    private Rigidbody rb;

    private bool ballThrown = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ballThrown) return;
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTime = Time.time;
                startPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTime = Time.time;

                timeDiff = endTime - startTime;
                endPos = touch.position;

                ThrowBall();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            ThrowBall();
        }
        
    }

    public void ThrowBall()
    {
        direction = startPos - endPos;

        if (direction.y > 50)
        {
            rb.useGravity = true;
            ballThrown = true;

            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;

            Vector3 launchDirection = cameraForward.normalized + Vector3.up * 0.5f;
            rb.AddForce(launchDirection * throwForce, ForceMode.Impulse);
            //rb.AddForce(direction.x * throwForce, direction.y * throwForce, throwForceZ / timeDiff);
            
            Destroy(gameObject, 5f);
        }
    }
}
