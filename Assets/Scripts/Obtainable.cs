using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObtainableTypes
{
    Rupees,
    Bombs,
    Arrows,
    Hearts
}

public class Obtainable : Trigger
{
    public ObtainableTypes type = ObtainableTypes.Rupees;
    public int amount = 1;
    public AudioSource pickupSound;

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
        if(pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound.clip, Camera.main.transform.position); //GameManager.instance.currentCamera.transform.position);
        }
        if(type == ObtainableTypes.Hearts)
        {
            Health playerHealth = player.gameObject.GetComponent<Health>();
            Debug.Log(playerHealth);
            if(playerHealth != null)
            {
                Debug.Log("Healing");
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
            Debug.Log("got " + this.name);
            
            Destroy(this.gameObject);
    }
}
