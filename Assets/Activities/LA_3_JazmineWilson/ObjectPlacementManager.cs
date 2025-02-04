using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject placementPrefab;
    [SerializeField] private ARRaycastManager  raycastManager;
    [SerializeField] private Transform reticle;

    private Vector2 midPoint;
    private List<ARRaycastHit> arRayCastHits = new List<ARRaycastHit>();
    private bool _canplaceobject = false;

    void Start()
    {
        midPoint = new Vector2(Screen.width / 2, Screen.height / 2);
    }
    

    // Update is called once per frame
    void Update()
    {
        ReticleUpdate();
    }

    private void ReticleUpdate()
    {
        if (raycastManager.Raycast(midPoint, arRayCastHits, TrackableType.Planes))
        {
            if (reticle)
            {
                  reticle.position = arRayCastHits[0].pose.position ;
            }
            _canplaceobject = true;
              
        }
        else
        {
            reticle.position = Vector3.one * 1000;
            _canplaceobject = false;
        }
    }
    public void PlaceObject()
    {
        if (!_canplaceobject) return;
        if (!placementPrefab) return;

        Instantiate(placementPrefab, arRayCastHits[0].pose.position, Quaternion.identity);
    }
}
