using UnityEngine;

public class PokeballSpawner : MonoBehaviour
{
    private bool _pokeballReleased = false;
    [SerializeField] private float _pokeballSpeed = 100;
    [SerializeField] private GameObject _pokeballPrefab;
    [SerializeField] private Camera _camera;
    private Vector3 _startTouchPosition = Vector3.zero;

    private float _mouseDragTime = 0.0f;
    [SerializeField] private float _minSpeed = 1000.0f;
    [SerializeField] private Vector3 _pokeballOffset = new Vector3(0, 0, 0);

    private GameObject _pokeball;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPokeball();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Application.isEditor)
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                {
                    _startTouchPosition = Input.touches[0].position;
                    _mouseDragTime = 0;
                }
                if(touch.phase == TouchPhase.Moved)
                {
                    _mouseDragTime += Time.deltaTime;
                }
                if(touch.phase == TouchPhase.Ended)
                {
                    Vector3 endTouchPosition = Input.touches[0].position;
                    OnTouchEnded(endTouchPosition);
                }
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                _startTouchPosition = Input.mousePosition;
                _mouseDragTime = 0;
            }
            if(Input.GetMouseButton(0))
            {
                _mouseDragTime += Time.deltaTime;
            }
            if(Input.GetMouseButtonUp(0))
            {
                Vector3 endTouchPosition = Input.mousePosition;
                OnTouchEnded(endTouchPosition);
            }
        }
    }

    private void OnTouchEnded(Vector3 endTouchPosition)
    {
        if(_pokeballReleased)
        {
            return;
        }

        Vector2 direction = endTouchPosition - _startTouchPosition;
        float distance = Vector3.Distance(endTouchPosition, _startTouchPosition);
        if((distance / _mouseDragTime > _minSpeed) && direction.y > 0)
        {

            Vector3 movement = _camera.transform.forward * _pokeballSpeed;
            movement.y += _pokeballSpeed;

            direction.Normalize();
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            Vector3 rotatedDirection = Quaternion.AngleAxis(-angle, Vector3.up) * movement;

            ThrowPokeball(rotatedDirection);
            Invoke("DestroyPokeball", 2f);
            Invoke("SpawnPokeball", 2f);
        }
    }

    private void ThrowPokeball(Vector3 direction)
    {
        _pokeballReleased = true;
        if(_pokeball)
        {
            _pokeball.transform.parent = null;  
            _pokeball.GetComponent<Rigidbody>().AddForce(direction);
            _pokeball.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void DestroyPokeball()
    {
        Destroy(_pokeball);
        _pokeball = null;
    }

    private void SpawnPokeball()
    {
        _pokeballReleased = false;
        if(!_pokeball)
        {
            _pokeball = Instantiate(_pokeballPrefab, _camera.transform.position, Quaternion.identity, _camera.transform);
            _pokeball.transform.localPosition = _pokeballOffset;
        }
    }
}
