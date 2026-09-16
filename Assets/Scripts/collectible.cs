using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static int Collected = 0;
    public static int Total = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Total++;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Collectible Acquired!");
            Collected++;
            Destroy(gameObject);
        }
    }
}
