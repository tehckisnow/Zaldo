using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public virtual void Activate1()
    {
        
    }

    public virtual void Activate()
    {
        Debug.Log(this.name + " activated.");
        //if(GameManager.instance.textbox.isOpen)
        GameManager.instance.textbox.Write("yah!!\nYa<color=green>h!</color> wolfenoot!");
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
