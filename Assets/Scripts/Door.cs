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
    
    private string id = "door";

    void Start()
    {
        GenerateId();
        if(GameManager.instance.RegisterData(id, isOpen))
        {
            isOpen = GameManager.instance.gameData[id];
        }
        if(isOpen)
        {
            OpenDoor();
        }
    }

    private void GenerateId()
    {
        //id += SceneManager.GetActiveScene().name += transform.position.x.ToString() += transform.position.y.ToString();
        string scenename = SceneManager.GetActiveScene().name;
        string x = transform.position.x.ToString();
        string y = transform.position.y.ToString();
        id += scenename += x += y;
    }

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
                GameManager.instance.SetData(id, true);
                player.obtainables[ObtainableTypes.Keys] -= 1;
                OpenDoor();
                isOpen = true;
            }
        }
    }

    private void OpenDoor()
    {
        GetComponent<SpriteRenderer>().sprite = openedSprite;
        GetComponent<Collider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
