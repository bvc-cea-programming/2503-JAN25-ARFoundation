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
    [SerializeField] private float gravity = 0.5f;
    
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
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTime = Time.time;
            
            startPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTime = Time.time;

            timeDiff = endTime - startTime;
            endPos = Input.GetTouch(0).position;;

            ThrowBall();
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
            rb.isKinematic = false;
            rb.isKinematic = true;
            ballThrown = true;
            
            rb.AddForce(direction.x * throwForce, direction.y * throwForce, throwForceZ / timeDiff);
            
            Destroy(gameObject, 5f);
        }
    }
}
