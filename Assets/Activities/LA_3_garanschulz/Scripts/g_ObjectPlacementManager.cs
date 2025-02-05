using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class g_ObjectPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject placePref;
    [SerializeField] private ARRaycastManager rMan;
    [SerializeField] private Transform ret;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject buttons;
    private GameObject chair;
    private Vector2 mPoint;
    private List<ARRaycastHit> rHits = new List<ARRaycastHit> ();
    private bool _canPlace = false;
    private bool _chairHeld = false;

    private void Start()
    {
        mPoint = new Vector2(Screen.width/2, Screen.height/2);
    }

    private void Update()
    {
        if (_chairHeld)
            ChairUpdate();
        else
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

    private void ChairUpdate()
    {
        if (rMan.Raycast(mPoint, rHits, TrackableType.PlaneWithinPolygon))
        {
            if (chair)
                chair.transform.position = rHits[0].pose.position;
        }
        else
        {
            chair.transform.position = Vector3.one * 10000;
            _canPlace = false;
        }
    }

    public void PlaceObject()
    {
        if (_chairHeld)
            return;
        if (!_canPlace)
            return;
        if (!placePref)
            return;
        Instantiate(placePref, rHits[0].pose.position, Quaternion.identity);
    }

    public void MouseClick()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 200f))
        {
            if (!hit.collider.gameObject.CompareTag("Chair"))
                return;
            if (_chairHeld)
            {
                _chairHeld = false;
                ret.gameObject.SetActive(true);
                buttons.gameObject.SetActive(false);
                return;
            }
            chair = hit.collider.gameObject;
            ret.gameObject.SetActive(false);
            buttons.gameObject.SetActive(true);
            _chairHeld = true;
        }
    }

    public void MouseRelease()
    {
    }

    public void Rotate(bool right)
    {
        if (right)
        {
            chair.transform.Rotate(0f, -15f, 0f);
        }
        else
        {
            chair.transform.Rotate(0f, 15f, 0f);
        }
    }
}
