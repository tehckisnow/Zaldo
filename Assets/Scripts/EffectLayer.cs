using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectLayer : MonoBehaviour
{
    public Color fadeColor = Color.black;

    private float fadeValue = 0f;
    private float fadeIncrement = 0.01f;
    private bool currentlyFading = false;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //manually set fade to static value
    public void SetFade(float value)
    {
        spriteRenderer.color = Color.Lerp(Color.clear, fadeColor, value);
    }

    //gradually fade out
    public void FadeOut(float time)
    {
        fadeValue = 0f;
        fadeIncrement = 1 / (time * 60);
        StartCoroutine(SetFadeOutOverTime());
    }

    public void FadeIn(float time)
    {
        fadeValue = 1f;
        fadeIncrement = 1 / (time * 60);
        StartCoroutine(SetFadeInOverTime());
    }

    IEnumerator SetFadeOutOverTime()
    {
        currentlyFading = true;
        while(fadeValue < 1)
        {
            fadeValue += fadeIncrement;
            SetFade(fadeValue);
            yield return null;
        }
        currentlyFading = false;
    }

    IEnumerator SetFadeInOverTime()
    {
        currentlyFading = true;
        while(fadeValue > 0)
        {
            fadeValue -= fadeIncrement;
            SetFade(fadeValue);
            yield return null;
        }
        currentlyFading = false;
    }
}
