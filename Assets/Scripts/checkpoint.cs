using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public DeathVoid deathVoid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            deathVoid.respawnPoint = transform;
            Debug.Log("Respawn Point...");
        }
    }
}