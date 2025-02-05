using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class g_CopymonPlacement : MonoBehaviour
{
    [SerializeField] private ARPlaneManager pMan;
    [SerializeField] private GameObject copymonP;

    private bool cPlaced = false;

    private void Start()
    {
        cPlaced = false;
    }

    private void FixedUpdate()
    {
        GenerateCopymon();
    }

    private void GenerateCopymon()
    {
        if (cPlaced)
            return;
        foreach(ARPlane plane in pMan.trackables)
        {
            if (cPlaced) 
                break;
            if (plane.alignment != PlaneAlignment.HorizontalUp)
                break;
            cPlaced = true;
            Instantiate(copymonP, plane.transform.position, Quaternion.identity);
        }
    }
}
