using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Trigger
{
    [SerializeField] private string destinationScene;
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float fadeTime = 2f;
    //soundEffect;
    
    [SerializeField] private bool isOpen = true;
    [SerializeField] private Sprite openedSprite;
    //private bool activated = false;
    
    public override void Activate()
    {
        if(isOpen)
        {
            GameManager.instance.fadeEffect.DoorTransition(x, y, destinationScene, fadeTime);
        }
        else
        {
            PlayerController player = GameManager.instance.player.GetComponent<PlayerController>();
            if(player.obtainables[ObtainableTypes.Keys] > 0)
            {
                player.obtainables[ObtainableTypes.Keys] -= 1;
                if(openedSprite != null)
                {
                    GetComponent<SpriteRenderer>().sprite = openedSprite;
                }
                isOpen = true;
                GetComponent<Collider2D>().isTrigger = true;
            }
        }
        
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
