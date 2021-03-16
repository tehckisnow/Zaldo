using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trigger
{
    [SerializeField]
    private int damage = 1;

    public override void Activate()
    {
        Health colliderHealth = GameManager.instance.player.GetComponent<Health>();
        colliderHealth.TakeDamage(damage);
        base.Activate();
    }
  // Start is called before the first frame update

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
