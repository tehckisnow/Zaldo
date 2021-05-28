using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EffectLayer : MonoBehaviour
{
    public Color fadeColor = Color.black;

    private float fadeValue = 0f;
    private float fadeIncrement = 0.01f;
    private SpriteRenderer spriteRenderer;
    private float fadeTime = 0;
    
    private float x = 0;
    private float y = 0;
    private string destinationScene = "";

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
        while(fadeValue < 1)
        {
            fadeValue += fadeIncrement;
            SetFade(fadeValue);
            yield return null;
        }
    }

    IEnumerator SetFadeInOverTime()
    {
        while(fadeValue > 0)
        {
            fadeValue -= fadeIncrement;
            SetFade(fadeValue);
            yield return null;
        }
    }

    public void DoorTransition(float xCoord, float yCoord, string destinationSceneName, float fadeTransitionTime)
    {
        fadeTime = fadeTransitionTime;
        x = xCoord;
        y = yCoord;
        destinationScene = destinationSceneName;

        StartCoroutine(Enter());
    }

    IEnumerator Enter()
    {
        //play sound effect

        //disable controls
        GameManager.instance.player.GetComponent<PlayerController>().controlsEnabled = false;
        //begin fade
        GameManager.instance.fadeEffect.FadeOut(fadeTime);
        //fade complete
        yield return new WaitForSeconds(fadeTime);
        //disable character
        GameManager.instance.player.SetActive(false);
        //load next scene
        if(destinationScene != "") //? also check for current scene;
        {
            SceneManager.LoadScene(destinationScene);
        }
        //position character and re-enable
        GameManager.instance.player.transform.position = new Vector3(x, y, 0f);
        GameManager.instance.player.SetActive(true);
        //begin fade in
        GameManager.instance.fadeEffect.FadeIn(fadeTime);
        //fade in complete
        //reenable controls
        GameManager.instance.player.GetComponent<PlayerController>().controlsEnabled = true;
        
        yield return null;
    }

}
