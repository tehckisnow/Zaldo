using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAnim : MonoBehaviour
{
    public float time = 200000f;
    public float whiteTime = 0.1f;
    public float triggerThreshold = 0.1f;
    [SerializeField] private Sprite spriteA;
    [SerializeField] private Sprite spriteB;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(time < triggerThreshold)
        {
            return;
        }
        else
        {
            spriteRenderer.sprite = spriteB;
            time = time/2;
            //whiteTime = whiteTime/2;
            StartCoroutine(Cycle(whiteTime));
        }
    }

    IEnumerator Cycle(float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRenderer.sprite = spriteA;
    }
}
