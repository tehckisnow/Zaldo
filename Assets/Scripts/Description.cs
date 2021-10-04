using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Description : Interaction
{

    public string description = "This is a sign.";
    
    public override void Activate()
    {
        GameManager.instance.player.GetComponent<PlayerController>().inputMode = InputMode.Text;
        GameManager.instance.textbox.Open(description);
    }
}
