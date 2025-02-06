using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject placementPrefab;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private Transform reticle;

    public GameObject placementPrefabClone;
    private Vector2 midPoint;
    private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();
    private bool _canPlaceObject = false;
    private bool _objectPlaced = false;

    private Touch touch;
    
    private float speed;

    void Start()
    {
        speed = 0.01f;
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

        if (_objectPlaced == false)
        {
            placementPrefabClone = Instantiate(placementPrefab, arRaycastHits[0].pose.position, Quaternion.identity);
            _objectPlaced = true;
        }
    }
}
