using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObtainableTypes
{
    Rupees,
    Bombs,
    Arrows,
    Hearts,
    Keys
}

public class Obtainable : Trigger, IPersist
{
    public ObtainableTypes type = ObtainableTypes.Rupees;
    public int amount = 1;
    public AudioSource pickupSound;

    public Persistence PersistenceComponent { get; set; }

    public override void Activate()
    {
        PlayerController player = GameManager.instance.player.GetComponent<PlayerController>();
        if(pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound.clip, Camera.main.transform.position); //GameManager.instance.currentCamera.transform.position);
        }
        if(type == ObtainableTypes.Hearts)
        {
            Health playerHealth = player.gameObject.GetComponent<Health>();
            if(playerHealth != null)
            {
                playerHealth.Heal(amount);
            }
        }
        else if(player.obtainables.ContainsKey(type))
            {
                player.obtainables[type] += amount;
            }
            else
            {
                //Debug error
                Debug.Log("Error: invalid obtainable type");
            }
            
            if(PersistenceComponent != null)
            {
                PersistenceComponent.SetState("main", true);
            }

            Destroy(this.gameObject);
    }

    public void TrueState()
    {
        Destroy(this.gameObject);
    }

    public void FalseState()
    {
        return;
    }
}
