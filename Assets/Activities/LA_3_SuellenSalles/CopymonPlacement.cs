using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CopymonPlacement : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private GameObject copymonPrefab;
    private bool _isCopymonPlaced = false;

    void Start()
    {
        _isCopymonPlaced = false;
    }

    void Update()
    {
        GenerateCopymon();
    }

    private void GenerateCopymon()
    {
        if (_isCopymonPlaced) return;

        foreach (ARPlane plane in planeManager.trackables)
        {
            if (_isCopymonPlaced) break;

            if (plane.alignment == PlaneAlignment.HorizontalUp)
            {
                _isCopymonPlaced = true;
	            Instantiate(copymonPrefab, plane.transform.position, Quaternion.identity);
            }
    	}
    }
}
