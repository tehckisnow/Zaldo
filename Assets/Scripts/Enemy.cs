using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        //animator.Play("hurt");
        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
