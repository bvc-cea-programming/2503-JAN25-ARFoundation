using System;
using UnityEngine;

public class ChairHandler : MonoBehaviour
{
    private OwenPlacementManager _placementManager;

    private void OnEnable()
    {
        _placementManager ??= FindFirstObjectByType<OwenPlacementManager>();
    }

    private void OnMouseDown()
    {
        _placementManager.PickUpObject(gameObject);
    }
}
