using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 1;
    private int health;

    [SerializeField]
    private float invincibilityTime = 1f;
    private bool invincible = false;

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
        if(!invincible)
        {
            invincible = true;
            health -= damage;
            if(health <= 0)
            {
                Die();
                return;
            }
            StartCoroutine(InvinsibilityTimer());
            StartCoroutine(DamageAnimation());
        }
    }

    IEnumerator InvinsibilityTimer()
    {
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }

    IEnumerator DamageAnimation()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.Play("TakeDamage");
        yield return new WaitForSeconds(0.2f);

        // SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        // sprite.color = Color.red;
        // yield return new WaitForSeconds(0.2f);
        // sprite.color = Color.white;
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
        StartCoroutine(DieAnimation());
    }

    IEnumerator DieAnimation()
    {
        //death animation
        Animator animator = gameObject.GetComponent<Animator>();
        animator.Play("Die");
        yield return new WaitForSeconds(0.5f);
        //drops

        //destroy
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
