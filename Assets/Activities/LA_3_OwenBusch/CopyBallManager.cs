using System;
using UnityEngine;

public class CopyBallManager : MonoBehaviour
{
    private Vector2 _mouseStartPos;
    private Vector2 _mouseEndPos;

    private Vector3 _startPos;
    private bool _holdingBall;
    private float _throwTime = 0f;

    private Rigidbody _rb;

    private Camera _parentCamera;
    
    [SerializeField] private float throwSpeed;
    [SerializeField] private float respawnTime;

    [SerializeField] private GameObject explosionEffect;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _parentCamera = GetComponentInParent<Camera>();
        _startPos = transform.localPosition;
    }

    private void Update()
    {
        if (_holdingBall)
        {
            _throwTime += Time.deltaTime;
        }
    }

    private void OnMouseDown()
    {
        _mouseStartPos = Input.mousePosition;
        _holdingBall = true;
        Debug.Log("Holding Ball");
    }

    private void OnMouseUp()
    {
        _mouseEndPos = Input.mousePosition;
        _holdingBall = false;
        _rb.isKinematic = false;
        _rb.AddForce(_parentCamera.transform.forward * CalculateThrowVelocity());
        _rb.AddForce(_parentCamera.transform.up * CalculateThrowVelocity()/2);
        Invoke(nameof(RespawnBall), respawnTime);
        Debug.Log("Dropped Ball");
        _throwTime = 0;
    }

    private float CalculateThrowVelocity()
    {
        return (_mouseEndPos.y - _mouseStartPos.y) / _throwTime * throwSpeed * Time.deltaTime;
    }

    private void RespawnBall()
    {
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.isKinematic = true;
        transform.localPosition = _startPos;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.GetComponent<CopyMon>()) return;
        Instantiate(explosionEffect, other.transform.position, Quaternion.identity);
        Destroy(other.gameObject);
    }
}
