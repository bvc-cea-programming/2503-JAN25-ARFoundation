using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CopymonPlacement : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private GameObject copymonPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool _isCopymonPlaced = false;
    void Start()
    {
        _isCopymonPlaced = false;
    }

    // Update is called once per frame
    void Update()
    {
        GenerateCopympn();
    }

    private void GenerateCopympn()
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
