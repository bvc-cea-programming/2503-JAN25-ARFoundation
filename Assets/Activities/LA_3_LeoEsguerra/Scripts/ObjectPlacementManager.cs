using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace LeoEsguerra
{
    public class ObjectPlacementManager : MonoBehaviour
    {
        public float rotationSpeed = 1.0f;
        public float movementSpeed = 1.0f;
        private bool _isObjectPlaced = false;
        private bool _canPlaceObject = false;
        [SerializeField] private GameObject _placementPrefab;
        private GameObject _placedObject;
        [SerializeField] private ARRaycastManager _raycastManager;
        [SerializeField] private Transform _reticle;
        private List<ARRaycastHit> _arRaycastHits = new List<ARRaycastHit>();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            ReticleUpdate();

            // Handle object placement on touch
            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if(!_isObjectPlaced)
                {
                    PlaceObject();
                }
            }

            // Handle object movement
            if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if(_isObjectPlaced)
                {
                    _placedObject.transform.position = new Vector3(_placedObject.transform.position.x + Input.GetTouch(0).deltaPosition.x * movementSpeed, _placedObject.transform.position.y, _placedObject.transform.position.z + Input.GetTouch(0).deltaPosition.y * movementSpeed);
                }
            }

            // Handle object rotation
            if(Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if(_isObjectPlaced)
                {
                    _placedObject.transform.rotation = Quaternion.Euler(0, _placedObject.transform.rotation.eulerAngles.y + Input.GetTouch(0).deltaPosition.x * rotationSpeed, 0);
                }
            }
        }

        private void ReticleUpdate()
        {
            if(!_isObjectPlaced && _raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), _arRaycastHits, TrackableType.PlaneWithinPolygon))
            {
                if(_reticle)
                {
                    _reticle.position = _arRaycastHits[0].pose.position;
                }

                _canPlaceObject = true;
            }
            else
            {
                if(_reticle)
                {
                    _reticle.position = Vector3.one * 1000;
                }

                _canPlaceObject = false;
            }
        }

        public void PlaceObject()
        {
            if(!_canPlaceObject || !_placementPrefab || _isObjectPlaced)
            {
                return;
            }

            _placedObject = Instantiate(_placementPrefab, _arRaycastHits[0].pose.position, Quaternion.identity);
            _isObjectPlaced = true;
        }
    }
}