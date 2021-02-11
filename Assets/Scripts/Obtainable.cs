using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObtainableTypes
{
    Rupees,
    Bombs,
    Arrows,
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
        //player.PlaySoundEffect(pickupSound);
        AudioSource.PlayClipAtPoint(pickupSound.clip, gameObject.transform.position);
        if(player.obtainables.ContainsKey(type))
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
