using System;
using UnityEngine;

public class Copymon : MonoBehaviour
{
    public Action OnCopymonCaughtlEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Contains("Pokeball"))
        {
            OnCopymonCaughtlEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
