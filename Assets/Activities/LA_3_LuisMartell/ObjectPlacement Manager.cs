using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject objectPlacementPrefab;
    
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private Transform reticle;
 

    private Vector2 midPoint;
    private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();
    private bool _canPlaceobject = false;


    void Awake() //Start
    {
        midPoint = new Vector2(Screen.width / 2, Screen.height / 2); 
        
    }

    void Update()
    {
        ReticleUpdate();

    }

    private void ReticleUpdate()
    {
        if (raycastManager.Raycast(
                midPoint,
                arRaycastHits,
                TrackableType.PlaneWithinPolygon
            ))
        {
            if (reticle)
            {
                reticle.position = arRaycastHits[0].pose.position;
            }

            _canPlaceobject = true;
            
        }

        else
        {
            reticle.position = Vector3.one * 100000;
            _canPlaceobject = false;

        }

    }

    public void PlaceObject()
    {
        if (!_canPlaceobject) return;
        if (!objectPlacementPrefab) return;

        
        Instantiate(objectPlacementPrefab, arRaycastHits[0].pose.position, Quaternion.identity);  

    }
}

