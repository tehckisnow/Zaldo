using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAnim : MonoBehaviour
{
    public float time = 8f;
    public float whiteTime = 0.1f;
    public float triggerThreshold = 0.01f;
    [SerializeField] private Sprite spriteA;
    [SerializeField] private Sprite spriteB;
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if(time < triggerThreshold)
        {
            return;
        }
        time = time/2f;
        StartCoroutine(Cycle(time));
    }

    IEnumerator Cycle(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("First delay: " + delay);
        spriteRenderer.sprite = spriteB;
        yield return new WaitForSeconds(whiteTime);
        spriteRenderer.sprite = spriteA;

        Debug.Log("after second delay");
    }
}
