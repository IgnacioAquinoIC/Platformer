using System.Collections;
using UnityEngine;

public class BlinkingPlatform : MonoBehaviour
{
    public float visibleTime = 3f;
    public float invisibleTime = 1f;
    public float startDelay = 0f;
    public void SetStartDelay(float delay)
    {
        startDelay = delay;
    }
    private Renderer platformRenderer;
    private Collider platformCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<Collider>();
        StartCoroutine(Blink());
    }
        IEnumerator Blink()
        {
            yield return new WaitForSeconds(startDelay);
            while (true)
            {
                platformRenderer.enabled = true;
                platformCollider.enabled = true;
                yield return new WaitForSeconds(visibleTime);

                platformRenderer.enabled = false;
                platformCollider.enabled = false;
                yield return new WaitForSeconds(invisibleTime);
            }
        }
}