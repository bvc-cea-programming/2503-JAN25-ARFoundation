using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        if (raycastManager.Raycast(midPoint, arRaycastHits, TrackableType.Planes))
        {
            if (reticle)
            {
                reticle.position = arRaycastHits[0].pose.position;
            }
            _canPlaceObject = true;
        }
        else
        {
            reticle.position = Vector3.one * 100000000;
            _canPlaceObject = false;
        }
    }

    public void PlaceObject()
    {
        if (!_canPlaceObject)
        {
            return;
        }

        if (!placementPrefab)
        {
            return;
        }

        Instantiate(placementPrefab, arRaycastHits[0].pose.position, Quaternion.identity);
    }
}
