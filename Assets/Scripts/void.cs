using UnityEngine;

public class DeathVoid : MonoBehaviour
{
    public Transform respawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = respawnPoint.position;
            other.transform.rotation = respawnPoint.rotation;
            Debug.Log("Respawning...");
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.linearVelocity = Vector3.zero;
            }
        }
    }
}