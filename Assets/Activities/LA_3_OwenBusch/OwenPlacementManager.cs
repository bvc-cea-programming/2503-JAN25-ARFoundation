using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class OwenPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject placementPrefab;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private Transform reticle;
    [SerializeField] private float rotateAmount;

    [SerializeField] private GameObject rotateMenu;

    private Vector2 midPoint;
    // We create a list of ARRaycastHits so that the AR Raycast manager can fill this list with the hitpoints.
    private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();

    private bool _canPlaceObject = false;

    private GameObject _spawnedObject;

    private bool _objectPlaced;

    void Start()
    {
        midPoint = new Vector2(Screen.width/2, Screen.height/2);
    }

    void Update()
    {
        ReticleUpdate();
    }

    private void ReticleUpdate()
    {
        if(raycastManager.Raycast(
                midPoint,
                arRaycastHits,
                TrackableType.PlaneWithinPolygon
        ))
        {
            if(reticle)
            {
                reticle.position = arRaycastHits[0].pose.position;
            }
            _canPlaceObject = true;
        }
        else{
            reticle.position = Vector3.one * 100000;
            _canPlaceObject = false;
        }
    }

    public void PlaceObject()
    {
        // return if cannot place object
        if(!_canPlaceObject) return;
        if(!placementPrefab) return;
        if(_objectPlaced) return;

        _objectPlaced = true;
        if (_spawnedObject)
        {
            _spawnedObject.transform.parent = null;
            rotateMenu.SetActive(false);
            return;
        }

        _spawnedObject = Instantiate(placementPrefab, arRaycastHits[0].pose.position, Quaternion.identity);
        
    }

    public void PickUpObject(GameObject objectToMove)
    {
        Debug.Log("picked up object");
        _spawnedObject = objectToMove;
        _spawnedObject.transform.SetParent(reticle.transform);
        _spawnedObject.transform.localPosition = Vector3.zero;
        _objectPlaced = false;
        rotateMenu.SetActive(true);
    }

    public void RotateObject(int direction)
    {
        if (!_spawnedObject) return;
        _spawnedObject.transform.Rotate(Vector3.up, rotateAmount * direction);
    }
}
