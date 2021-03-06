﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int lifetime = 300;
    public int damage = 1;

    private Collider2D interactionPoint;

    // Start is called before the first frame update
    void Start()
    {
        interactionPoint = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();

        if(lifetime > 0)
        {
            lifetime--;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Attack()
    {
        List<Collider2D> results = new List<Collider2D>();
        if(interactionPoint.OverlapCollider(new ContactFilter2D(), results) > 0)
        {
            foreach(Collider2D current in results)
            {
                Health foundTarget = current.gameObject.GetComponent<Health>();
                if(foundTarget != null)
                {
                    foundTarget.TakeDamage(damage);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
