using UnityEngine;

public class Finish : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.linearVelocity = Vector3.zero;
                rigidbody.isKinematic = true;
            }

            Debug.Log("Level Complete! Colletibles Obtained: " + Collectible.Collected + " / " + Collectible.Total);
        }
    }
}