using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Trigger
{
    public string destinationScene;
    public float x;
    public float y;
    public float fadeTime = 2f;
    //soundEffect;
    
    private bool activated = false;

    public override void Activate()
    {
        GameManager.instance.fadeEffect.DoorTransition(x, y, destinationScene, fadeTime);
        
        //if(!activated)
        {
            //activated = true;
            //StartCoroutine(Enter());
        }
    }

    // IEnumerator Enter()
    // {
    //     //play sound effect

    //     //disable controls
    //     GameManager.instance.player.GetComponent<PlayerController>().controlsEnabled = false;
    //     //begin fade
    //     GameManager.instance.fadeEffect.FadeOut(fadeTime);
    //     //fade complete
    //     yield return new WaitForSeconds(fadeTime);
    //     //disable character
    //     GameManager.instance.player.SetActive(false);
    //     //load next scene
    //     if(destinationScene != "") //? also check for current scene;
    //     {
    //         //SceneManager.LoadScene(destinationScene);
    //     }
    //     //position character and re-enable
    //     GameManager.instance.player.transform.position = new Vector3(x, y, 0f);
    //     GameManager.instance.player.SetActive(true);
    //     //begin fade in
    //     GameManager.instance.fadeEffect.FadeIn(fadeTime);
    //     //fade in complete
    //     //reenable controls
    //     GameManager.instance.player.GetComponent<PlayerController>().controlsEnabled = true;
    //     activated = false;
        
    //     yield return null;
    // }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
