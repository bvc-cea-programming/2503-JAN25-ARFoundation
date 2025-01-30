using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class KaijuPlacement : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private GameObject kaijuPrefab;
    private bool _isKaijuPlaced = false;

    private void Update()
    {
        GenerateKaiju();
    }
    private void GenerateKaiju()
    {
        if (_isKaijuPlaced) return;

        foreach (ARPlane plane in planeManager.trackables)
        {
            if (_isKaijuPlaced) break;
            if(plane.alignment == PlaneAlignment.HorizontalUp)
            _isKaijuPlaced = true;
            Instantiate(kaijuPrefab, plane.transform.position, Quaternion.identity);
        }
    }

}
