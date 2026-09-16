using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public float travelSpeed = 2f;
    public float travelDistance = 5f;
    private Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
        Debug.Log("MobilePlatform started");
        Debug.Log(rigidbody);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movement = Mathf.PingPong(Time.time * travelSpeed, travelDistance);
        transform.position = startPosition + Vector3.right * movement;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
