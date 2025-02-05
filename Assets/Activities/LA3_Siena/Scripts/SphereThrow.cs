using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SphereThrow : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager raycastManager;
    
    [Header ("Sphere Values")]
    [SerializeField]
    private GameObject throwableSphere;
    [SerializeField]
    private float distanceOfSphereFromCamera = 5f;
    [SerializeField]
    private float sphereVelocity;
    [SerializeField]
    private float sphereSpeed;
    [SerializeField]
    private float maxSphereSpeed;

    [Header ("User Values")]
    [SerializeField]
    private float smoothing;
    [SerializeField]
    private float minimumThrowDistance;
    
    [SerializeField]
    private float rayCastLength = 100f;

    private bool holdingSphere;
    private bool sphereThrown;

    private float startTime, endTime, throwTime;
    private float throwDistance;

    private Rigidbody sphereRigidbody;

    private Vector2 startPosition;
    private Vector2 endPositition;
    private Vector2 midpoint;

    private Vector3 newPosition;
    private Vector3 angle;
    private Vector3 mousePos;

    private List <ARRaycastHit> aRRaycastHits = new List<ARRaycastHit>();

    void Start()
    {
        sphereRigidbody = GetComponent<Rigidbody>();
        sphereRigidbody.useGravity = false;
        midpoint = new Vector2(Screen.width/2,Screen.height/2);
    }

    void Update()
    {
        mousePos = Input.mousePosition;

        if(holdingSphere == true)
            PickUpSphere();

        if(sphereThrown == true)
            return;

        if(Input.GetMouseButtonDown(0) && raycastManager.Raycast(midpoint, aRRaycastHits, TrackableType.PlaneWithinPolygon))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if(Physics.Raycast(mouseRay, out hit, rayCastLength))
            {
                if(hit.transform == throwableSphere.transform)
                {
                    startTime = Time.time;
                    startPosition = Input.mousePosition;
                    holdingSphere = true;
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            endTime = Time.time;
            endPositition = Input.mousePosition;
            throwDistance  = (endPositition - startPosition).magnitude; 
            throwTime = endTime - startTime;

            if(throwTime < 0.5f && throwDistance > 30f)
            {
                CalculateSphereSpeed();
                CalculateSphereAngle();

                sphereRigidbody.AddForce(new Vector3 (angle.x * sphereSpeed, (angle.y * sphereSpeed / 3), (angle.z * sphereSpeed) * 2));
                sphereRigidbody.useGravity = true;
                holdingSphere = false;
                sphereThrown = true;
            }
        }
    }

    void CalculateSphereSpeed()
    {
        if(throwDistance < 0)
            sphereVelocity = throwDistance / (throwDistance - throwTime);

        sphereSpeed = sphereVelocity * 50f;

        if(sphereSpeed >= maxSphereSpeed)
            sphereSpeed = maxSphereSpeed;
    }

    void CalculateSphereAngle()
    {
        angle = Camera.main.ScreenToWorldPoint(new Vector3(endPositition.x, endPositition.y, Camera.main.nearClipPlane + 5f));
    }

    void PickUpSphere()
    {
        mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane * distanceOfSphereFromCamera;
        newPosition = Camera.main.ScreenToWorldPoint(mousePos);
        throwableSphere.transform.position = Vector3.Lerp(throwableSphere.transform.position, newPosition, 80f * Time.deltaTime);
    }
}
