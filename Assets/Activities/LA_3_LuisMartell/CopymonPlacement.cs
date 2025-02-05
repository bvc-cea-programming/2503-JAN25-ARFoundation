using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CopymonPlacement : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private GameObject copymonPrefab;
    private bool _iscopymonPlaced = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()// THESE ARE CALLED METHODS LOL
    {
        _iscopymonPlaced = false;
    }

    // Update is called once per frame
    void Update()
    {
        GenerateCopymon();
    }

    private void GenerateCopymon()
    {
        if(_iscopymonPlaced) return;
        foreach (ARPlane plane in planeManager.trackables) 
        {
            if(_iscopymonPlaced) break;
            if (plane.alignment == PlaneAlignment.HorizontalUp)
            {
              _iscopymonPlaced = true;
                         
              Instantiate(copymonPrefab, plane.transform.position, Quaternion.identity);  
            }
        }
    }
}
