using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    [SerializeField] private ARRaycastManager raycastManager;

    [SerializeField] private Transform reticle;

    private Vector2 midPoint;

    private List<ARRaycastHit> _arRaycastHits = new List<ARRaycastHit>();

    private bool CanPlace = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        if (raycastManager.Raycast(midPoint, _arRaycastHits, TrackableType.PlaneWithinPolygon))
        {
            if (reticle)
            {
                reticle.position = _arRaycastHits[0].pose.position;
                
            }
            CanPlace = true;
        }
        else
        {
            CanPlace = false;
            reticle.position = Vector3.one * 10000;
        }
    }

    public void PlaceObj()
    {
        if(!CanPlace) return;
        
        if(!prefab) return;

        Instantiate(prefab, _arRaycastHits[0].pose.position, Quaternion.identity);
    }
}
