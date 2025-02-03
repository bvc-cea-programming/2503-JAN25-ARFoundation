using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DraggingObject : MonoBehaviour
{
    [SerializeField]
    private GameObject placedObject;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private ARRaycastManager raycastManager;
    
    //[SerializeField]
    //private float maxDistanceOnRaySelection = 25.0f;

    private List <ARRaycastHit> aRRaycastHits = new List<ARRaycastHit>();

    private Vector2 touchPosition;
    private Touch touch;
    private bool onTouchHold = false;

    void Update()
    {
        //touch = Input.GetTouch(0);

        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchPosition = touch.position;

            if(touch.phase == TouchPhase.Began)
            {
                Ray ray = mainCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject; 

                if(Physics.Raycast(ray, out hitObject))
                {
                    if(hitObject.transform.name.Contains("PlacedObject"))
                    {
                        onTouchHold = true;
                    }
                }
            }

            if(touch.phase == TouchPhase.Ended)
            {
                onTouchHold = false;
            }
        }

        
        if(raycastManager.Raycast(touchPosition, aRRaycastHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = aRRaycastHits[0].pose;

            //if(placedObject == null)
                //Instantiate(placedObject, hitPose.position, hitPose.rotation);

            //else
            //{
                if(onTouchHold == true)
                {
                    placedObject.transform.position = hitPose.position;
                    placedObject.transform.rotation = hitPose.rotation;
                }
            //}
        }
    }
}
