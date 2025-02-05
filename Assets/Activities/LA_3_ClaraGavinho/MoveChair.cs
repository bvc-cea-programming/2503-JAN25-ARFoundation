using UnityEngine;

public class MoveChair : MonoBehaviour
{
    private Touch touch;
    
    private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        ChairMovement();
    }

    void ChairMovement()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speed,
                    transform.position.y, transform.position.z + touch.deltaPosition.y * speed);
            }
        }

        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            transform.Rotate(0f, touch.deltaPosition.y, 0f);
        }
    }
}
