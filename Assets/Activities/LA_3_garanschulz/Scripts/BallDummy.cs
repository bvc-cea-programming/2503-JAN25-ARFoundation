using UnityEngine;

public class BallDummy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Copymon"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
