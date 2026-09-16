using UnityEngine;

public class BlinkingPlatformGroup : MonoBehaviour
{
    public float delayBetweenPlatforms = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BlinkingPlatform[] platforms = GetComponentsInChildren<BlinkingPlatform>();
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].SetStartDelay(i * delayBetweenPlatforms);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
