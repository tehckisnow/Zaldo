//! consider making an interface

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    bool active = false;

    public virtual void Activate()
    {
        if(!active)
        {
            active = true;
            Debug.Log(this.name + " triggered.");
        }
    }

    public virtual void Callback()
    {

    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     PlayerController controller = other.GetComponent<PlayerController>();

    //     if (controller != null)
    //     {
    //         //if(controller.currentHealth < controller.maxHealth)
    //         //{
    //         //controller.ChangeHealth(1);
    //         Destroy(gameObject);
    //         //}
    //     }
    // }
}
