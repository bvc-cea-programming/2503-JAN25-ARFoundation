using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectPlacementManager : MonoBehaviour
{
    [SerializeField]
    private GameObject placementObject;

    [SerializeField]
    private ARRaycastManager raycastManager;

    [SerializeField]
    private Transform rectile;

    private Vector2 midpoint;
    private List <ARRaycastHit> aRRaycastHits = new List<ARRaycastHit>();
    private bool placeObject = false;

    void Start()
    {
        midpoint = new Vector2(Screen.width/2,Screen.height/2);
    }

    void Update()
    {
        RectileUpdate();
    }

    private void RectileUpdate()
    {
        if(raycastManager.Raycast(midpoint, aRRaycastHits, TrackableType.PlaneWithinPolygon))
        {
            placeObject = true;

            if(rectile)
                rectile.position = aRRaycastHits[0].pose.position;
                
            else
            {
                rectile.position = Vector3.one * 10000;
                placeObject = false;
            }
                
        }
    }

    public void PlaceObject()
    {
        if(!placeObject)
            return;

        if(!placementObject)
            return;

        Instantiate(placementObject, aRRaycastHits[0].pose.position, Quaternion.identity);
    }
}
