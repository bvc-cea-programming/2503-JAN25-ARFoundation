using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;

public class MoveChair : MonoBehaviour
{
    //private Touch touch;

    public ARRaycastManager _raycastManager;

    private List<ARRaycastHit> _rayList = new List<ARRaycastHit>();

    private Vector3 _mousePos;

    private bool _isBeingMoved = false;

    private GameObject chair;

    [SerializeField ]private ObjectPlacementManager _placementScript;

    // Update is called once per frame
    void Update()
    {
        _mousePos = Input.mousePosition;
        ChairMovement();
    }

    void ChairMovement()
    {
        //touch = Input.GetTouch(0);
        
        chair = _placementScript.placementPrefabClone;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray raycast = Camera.main.ScreenPointToRay(_mousePos);
            
            if (Physics.Raycast(raycast, out hit))
            {
                if (hit.collider.CompareTag("Chair"))
                    _isBeingMoved = true;
            }
            
        }

        if (_raycastManager.Raycast(_mousePos, _rayList, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose pose = _rayList[0].pose;

            if (_isBeingMoved == true && chair != null)
            {
                chair.transform.position = pose.position;
                chair.transform.rotation = pose.rotation;
            }
            
            /*
            if (Input.GetMouseButtonDown(0))
            {
                chair.transform.position = new Vector3(transform.position.x + _mousePos.x * speed,
                    transform.position.y, transform.position.z + _mousePos.y * speed);
            }
            */
            
        }

        
        if (Input.GetMouseButtonUp(0))
        {
            _isBeingMoved = false;
            //chair.transform.Rotate(_mousePos.x, 0f, 0f);
        }
        
        
    }
    
}
