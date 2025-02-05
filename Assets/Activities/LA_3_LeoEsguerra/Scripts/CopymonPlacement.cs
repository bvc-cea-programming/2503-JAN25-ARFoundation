using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace LeoEsguerra
{
    public class CopymonPlacement : MonoBehaviour
    {
        private bool _isCopymonPlaced = false;
        [SerializeField] private GameObject _copymonPrefab;
        [SerializeField] private ARPlaneManager _planeManager;
        public GameObject textCaught;
        [SerializeField] private Camera _camera;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _isCopymonPlaced = false;
        }

        // Update is called once per frame
        void Update()
        {
            GenerateCopymon();
        }

        private void GenerateCopymon()
        {
            if(_isCopymonPlaced)
            {
                return;
            }

            foreach (ARPlane plane in _planeManager.trackables)
            {
                if(_isCopymonPlaced)
                {
                    break;
                }

                if(plane.alignment == PlaneAlignment.HorizontalUp)
                {
                    _isCopymonPlaced = true;
                    
                    // Face the camera
                    Quaternion targetDir = Quaternion.LookRotation(_camera.transform.position - plane.transform.position);
                    GameObject copymon = Instantiate(_copymonPrefab, plane.transform.position, targetDir);
                    copymon.GetComponent<Copymon>().OnCopymonCaughtlEvent += OnCopymonCaught;
                }
            }
        }
        
        private void OnCopymonCaught()
        {
            textCaught.SetActive(true);
            Invoke("ResetText", 2.0f);
        }

        private void ResetText()
        {
            textCaught.SetActive(false);
            _isCopymonPlaced = false;
        }
    }
}