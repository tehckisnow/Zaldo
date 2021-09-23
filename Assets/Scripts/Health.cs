using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
            //check if player and is carrying a liftable; if so, throw it
            PlayerController playerController = gameObject.GetComponent<PlayerController>();
            if(playerController?.carriedItem != null)
            {
                playerController.carriedItem.GetComponent<Liftable>().ThrowObject(playerController.facing);
            }

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
        if(animator != null)
        {
            //animator.Play("TakeDamage");
            animator.SetTrigger("Hurt");
        }
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
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Knockback kb = GetComponent<Knockback>();
        Destroy(kb);
        Destroy(rb);
        StartCoroutine(DieAnimation());
    }

    IEnumerator DieAnimation()
    {
        //death animation
        Animator animator = gameObject.GetComponent<Animator>();
        animator.Play("Die");
        yield return new WaitForSeconds(0.5f);
        //drops

        if(gameObject.GetComponent<PlayerController>() != null)
        {
            SceneManager.LoadScene("GameOver");
        }
        //destroy
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

}
