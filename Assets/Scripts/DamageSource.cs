using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float knockbackForce = 1;
    [SerializeField] private bool destroyOnCollision = false;

    private Collider2D coll2D;

    // Start is called before the first frame update
    void Start()
    {
        coll2D = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();
    }

    private void CheckCollision()
    {
        Collider2D[] results = new Collider2D[5];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.NoFilter();
        int numberOfResults = coll2D.OverlapCollider(contactFilter, results);
        int current = 0;
        while(current < numberOfResults)
        {
            //check for health component
            Health health = results[current].gameObject.GetComponent<Health>();
            if(health != null)
            {
                health.TakeDamage(damage);
            }
            //check for knockback component
            Knockback knockback = results[current].gameObject.GetComponent<Knockback>();
            if(knockback != null)
            {
                knockback.TakeKnockback(transform.position, knockbackForce);
            }
            current++;
        }
        if(destroyOnCollision && numberOfResults > 0)
        {
            Explode explode = gameObject.GetComponent<Explode>();
            if(explode != null)
            {
                explode.Activate();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
