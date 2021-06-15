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
    [SerializeField] private Sprite closedSprite;

    private string id = "door";
    public Persistence persistence;

    void Start()
    {
        //GenerateId();
        //SetInitialState();
    }

    // private void SetInitialState()
    // {
    //     if(GameManager.instance.persistentData.Exists(id, "isOpen"))
    //     {
    //         if(GameManager.instance.persistentData.GetData(id, "isOpen"))
    //         {
    //             OpenDoor();
    //         }
    //         else
    //         {
    //             CloseDoor();
    //         }
    //     }
    //     else
    //     {
    //         GameManager.instance.persistentData.RegisterValue(id, "isOpen", isOpen);
    //     }
    // }

    // private void GenerateId()
    // {
    //     //id += SceneManager.GetActiveScene().name += transform.position.x.ToString() += transform.position.y.ToString();
    //     string scenename = SceneManager.GetActiveScene().name;
    //     string x = transform.position.x.ToString();
    //     string y = transform.position.y.ToString();
    //     id += scenename += x += y;
    // }

    // private void SetState(string key, bool state)
    // {
    //     GameManager.instance.persistentData.RegisterValue(id, key, state);
    // }

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
                
                //GameManager.instance.persistentData.RegisterValue(id, "isOpen", true);
                persistence.SetState("main", true);
                //gameObject.GetComponent<Persistence>().SetState("main", true);

                player.obtainables[ObtainableTypes.Keys] -= 1;
                //OpenDoor();
                TrueState();
            }
        }
    }

    // private void OpenDoor()
    // {
    //     GetComponent<SpriteRenderer>().sprite = openedSprite;
    //     GetComponent<Collider2D>().isTrigger = true;
    //     isOpen = true;
    // }

    // private void CloseDoor()
    // {
    //     GetComponent<SpriteRenderer>().sprite = closedSprite;
    //     GetComponent<Collider2D>().isTrigger = false;
    //     isOpen = false;
    // }

//!----------------------------------
    public void TrueState()
    {
        GetComponent<SpriteRenderer>().sprite = openedSprite;
        GetComponent<Collider2D>().isTrigger = true;
        isOpen = true;
    }

    public void FalseState()
    {
        GetComponent<SpriteRenderer>().sprite = closedSprite;
        GetComponent<Collider2D>().isTrigger = false;
        isOpen = false;
    }
}
