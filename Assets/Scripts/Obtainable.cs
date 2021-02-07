﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obtainable : Trigger
{
    public string type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate()
    {
        PlayerController player = GameManager.instance.player.GetComponent<PlayerController>();

        switch(type)
        {
            case "rupee":
                player.rupees++;
                break;
            case "arrow":
                player.arrows++;
                break;
            default:
                break;
        }
        Debug.Log("got");
        Destroy(this.gameObject);
    }
}
