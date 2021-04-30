using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    public virtual void Activate()
    {
        Debug.Log(this.name + " activated.");

        //!input mode
        //mode = "textbox";
        //Action callback = RevertMode;
        //!method for calling textbox.advance in input system
        //!method RevertMode
        GameManager.instance.textbox.Open("yah!!\nYa<color=green>h!</color> wolfenoot!");
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
