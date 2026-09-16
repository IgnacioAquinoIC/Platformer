using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public float travelSpeed = 2f;
    public float travelDistanceX = 5f;
    public float travelDistanceZ = 5f;
    private Vector3 startPosition;
    private int movementPhase = 0;
    private PlayerMovement player;
    float degrees = 90f;
    bool freeze = false;
    private Quaternion targetRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        targetRotation = Quaternion.Euler(0f, 0f, 0f);
        rigidbody = GetComponent<Rigidbody>();
        Debug.Log("RotatingPlatform started");
        Debug.Log(rigidbody);
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (freeze) return;
        Vector3 targetPosition = startPosition;
        Quaternion lasttargetRotation = targetRotation;

        switch (movementPhase)
        {
            case 0:
                targetPosition += Vector3.right * travelDistanceX;
                targetRotation = Quaternion.Euler(0f, 0f, 0f);
                break;

            case 1:
                targetPosition += Vector3.right * travelDistanceX;
                targetPosition += Vector3.forward * travelDistanceZ;
                targetRotation = Quaternion.Euler(0f, 90f, 0f);
                break;

            case 2:
                targetPosition += Vector3.forward * travelDistanceZ;
                targetRotation = Quaternion.Euler(0f, 180f, 0f);
                break;

            case 3:
                targetPosition = startPosition;
                targetRotation = Quaternion.Euler(0f, 270f, 0f);
                break;
        }
        //if(lasttargetRotation != targetRotation)
        //{
            //player.canJumpRotation = false;
        //}
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, travelSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, degrees * Time.fixedDeltaTime);

        bool reachedPosition =
        Vector3.Distance(transform.position, targetPosition) < 0.01f;
        bool reachedRotation =
        Quaternion.Angle(transform.rotation, targetRotation) < 0.1f;


        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            movementPhase++;
            StartCoroutine(Freeze());
            //player.canJumpRotation = true;

            if (movementPhase > 3)
            {
                movementPhase = 0;
            }
        }
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

    IEnumerator Freeze()
    {
        if (freeze) yield return null;
        freeze = true;
        yield return new WaitForSeconds(1.5f);
        freeze = false;
    }
}
