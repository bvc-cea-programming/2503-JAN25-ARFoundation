using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private ARPlaneManager planeManager;

    [SerializeField]
    private GameObject monster;

    public bool hasMonsterBeenSpawned = false;

    void Start()
    {
        
    }

    void Update()
    {
        GenerateMonster();
    }

    private void GenerateMonster()
    {
        if(hasMonsterBeenSpawned)
            return;
        
        foreach(ARPlane plane in planeManager.trackables)
        {
            if(hasMonsterBeenSpawned)
                break; 

            if(plane.alignment == PlaneAlignment.HorizontalUp)
            {
                hasMonsterBeenSpawned = true;
                Instantiate(monster, plane.transform.position, Quaternion.identity);
            }

            
        }
    }

}
