using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DraggingObject : MonoBehaviour
{
    public GameObject placedObject; //Influenced by the "ObjectPlacementManager"

    [SerializeField]
    private ARRaycastManager raycastManager;

    private List <ARRaycastHit> aRRaycastHits = new List<ARRaycastHit>();
    private Vector3 mousePos; //Mouse cursor position on the screen
    private Vector2 touchPosition; //To find the coordinates of your touch on a screen
    private Touch touch; //For a touch input on a mobile device
    private bool onTouchHold = false;

    void Update()
    {
        //Debug.Log(onTouchHold);

        mousePos = Input.mousePosition;

        //touch = Input.GetTouch(0);

        //if(Input.touchCount > 0)
        //{
            //touch = Input.GetTouch(0);
            //touchPosition = touch.position;

            //if(touch.phase == TouchPhase.Began)
            if(Input.GetMouseButtonDown(0))
            {
                onTouchHold = true;
    
                //Ray ray = Camera.main.ScreenPointToRay(touch.position);
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                RaycastHit hitObject; 

                if(Physics.Raycast(ray, out hitObject))
                {
                    if(hitObject.transform.name.Contains("PlacedObject"))
                        onTouchHold = true;
                }
            }

            //if(touch.phase == TouchPhase.Ended)
            if(Input.GetMouseButtonUp(0))
                onTouchHold = false;
        //}

        
        if(raycastManager.Raycast(mousePos, aRRaycastHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = aRRaycastHits[0].pose;

            //if(placedObject == null)
                //Instantiate(placedObject, hitPose.position, hitPose.rotation);

            //else
            //{
                if(onTouchHold == true && placedObject != null)
                {
                    placedObject.transform.position = hitPose.position;
                    placedObject.transform.rotation = hitPose.rotation;
                }
            //}
        }
    }
}
