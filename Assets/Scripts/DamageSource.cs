using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float knockbackForce = 1;

    // public override void Activate()
    // {
    //     Health colliderHealth = GameManager.instance.player.GetComponent<Health>();
    //     colliderHealth.TakeDamage(damage);
    //     base.Activate();
    // }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.collider.gameObject.GetComponent<Health>();
        if(health != null)
        {
            health.TakeDamage(damage);
            
        }
        Knockback knockback = collision.collider.gameObject.GetComponent<Knockback>();
        if(knockback != null)
        {
            knockback.TakeKnockback(transform.position, knockbackForce);
        }
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
