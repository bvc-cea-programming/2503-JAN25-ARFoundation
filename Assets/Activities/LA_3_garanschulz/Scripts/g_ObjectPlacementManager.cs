using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class g_ObjectPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject placePref;
    [SerializeField] private ARRaycastManager rMan;
    [SerializeField] private Transform ret;
    private Vector2 mPoint;
    private List<ARRaycastHit> rHits = new List<ARRaycastHit> ();
    private bool _canPlace = false;

    private void Start()
    {
        mPoint = new Vector2(Screen.width/2, Screen.height/2);
    }

    private void Update()
    {
        ReticleUpdate();
    }
    private void ReticleUpdate()
    {
        if (rMan.Raycast(mPoint, rHits, TrackableType.PlaneWithinPolygon))
        {
            if(ret)
                ret.position = rHits[0].pose.position;
            _canPlace = true;
        }
        else
        {
            ret.position = Vector3.one * 10000;
            _canPlace = false;
        }
    }

    public void PlaceObject()
    {
        if (!_canPlace)
            return;
        if (!placePref)
            return;
        Instantiate(placePref, rHits[0].pose.position, Quaternion.identity);
    }
}
