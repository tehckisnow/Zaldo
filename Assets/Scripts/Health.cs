using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 1;
    [SerializeField]
    private int health;

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
        StartCoroutine(DamageAnimation());
    }

    IEnumerator DamageAnimation()
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }

    public virtual void Heal(int amount)
    {
        health += amount;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public virtual void FullHeal()
    {
        health = maxHealth;
    }

    public virtual void Die()
    {
        //death animation
        //drops
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
