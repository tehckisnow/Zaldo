﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Trigger, IPersist
{
    [SerializeField] private string destinationScene;
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float fadeTime = 2f;
    //soundEffect;
    
    [SerializeField] private bool isOpen = true;
    [SerializeField] private Sprite openedSprite;
    [SerializeField] private Sprite closedSprite;

    public Persistence PersistenceComponent { get; set; }

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
                PersistenceComponent.SetState("main", true);
                player.obtainables[ObtainableTypes.Keys] -= 1;
                TrueState();
            }
        }
    }

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
