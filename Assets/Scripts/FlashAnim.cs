using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAnim : MonoBehaviour
{
    public float time = 2000000f;
    public float whiteTime = 0.1f;
    public float triggerThreshold = 0.001f;
    [SerializeField] private Sprite spriteA = null;
    [SerializeField] private Sprite spriteB = null;
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
        spriteRenderer.sprite = spriteA;
        yield return new WaitForSeconds(whiteTime);
        spriteRenderer.sprite = spriteB;

    }
}
