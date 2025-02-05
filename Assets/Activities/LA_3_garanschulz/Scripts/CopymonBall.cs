using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CopymonBall : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private GameObject ballTarget;
    private Vector2 clickStart;
    private Vector2 clickEnd;
    public int ballTimer;


    private bool clicking = false;

    public void MouseClick() 
    {
        clickStart = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f))
        {
            if (!hit.collider.gameObject.GetComponent<BallDummy>())
                return;
            ballTarget = hit.collider.gameObject;
            clicking = true;
        }
    }

    public void MouseRelease()
    {
        clicking = false;
        clickEnd = Input.mousePosition;
        float dist = Mathf.Sqrt((clickEnd.x - clickStart.x) + (clickEnd.y - clickStart.y));
        //Debug.Log("sqrt= " +  dist);
        float speed = dist / ballTimer;
        ballTimer = 0;
        if (speed > 2f)
            LaunchBall(speed);
    }

    private void FixedUpdate()
    {
        BallTimer();
    }

    private void BallTimer()
    {
        if (!clicking)
            return;
        ballTimer++;
    }

    private void LaunchBall(float speed)
    {
        if (ballTarget == null)
            return;
        Rigidbody rb = ballTarget.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(cam.transform.forward * (speed * 2), ForceMode.Impulse);
    }
}
