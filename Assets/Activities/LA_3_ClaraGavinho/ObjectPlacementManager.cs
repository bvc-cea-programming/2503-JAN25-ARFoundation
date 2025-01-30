using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject placementPrefab;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private Transform reticle;

    private Vector2 midPoint;
    private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();
    private bool _canPlaceObject = false;

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
        if (raycastManager.Raycast(midPoint, arRaycastHits, TrackableType.PlaneWithinPolygon))
        {
            if (reticle)
            {
                reticle.position = arRaycastHits[0].pose.position;
            }

            _canPlaceObject = true;
        }
        else
        {
            reticle.position = Vector3.one * 10000;
            _canPlaceObject = false;
        }
    }

    public void PlaceObject()
    {
        // return if cannot place object
        if (!_canPlaceObject) return;

        if (!placementPrefab) return;

        Instantiate(placementPrefab, arRaycastHits[0].pose.position, Quaternion.identity);
    }
}
