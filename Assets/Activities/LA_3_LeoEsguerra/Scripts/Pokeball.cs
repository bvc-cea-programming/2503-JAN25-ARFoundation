using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Pokeball : MonoBehaviour
{
    [SerializeField] private float _life = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Invoke("DestroyPokeball", _life);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Contains("Cat"))
        {
            DestroyPokeball();
        }
    }

    private void DestroyPokeball()
    {
        Destroy(gameObject);
    }
}
