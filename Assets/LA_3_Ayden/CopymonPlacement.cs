using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CopymonPlacement : MonoBehaviour
{

    [SerializeField] private ARPlaneManager arPlaneManager;

    [SerializeField] private GameObject copyMonPrefab;

    private bool _isCopymonPlaced = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isCopymonPlaced = false;
    }

    // Update is called once per frame
    void Update()
    {
        GenerateCopyMon();
    }

    private void GenerateCopyMon()
    {
        if (_isCopymonPlaced) return;
        foreach (ARPlane plane in arPlaneManager.trackables)
        {
            if(_isCopymonPlaced) break;

            if (plane.alignment == PlaneAlignment.HorizontalUp)
            {
                _isCopymonPlaced = true;
                Instantiate(copyMonPrefab, plane.transform.position, Quaternion.identity);
            }
        }
    }
}
