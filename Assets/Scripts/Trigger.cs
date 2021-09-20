//! consider making an interface
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool active = true;
    //public bool disableAfterTrigger = true;

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public virtual void Activate()
    {
        if(active)
        {
            active = false;
            Debug.Log(this.name + " triggered.");
            //call callback here?
        }
    }

    public virtual void Callback()
    {

    }

}
