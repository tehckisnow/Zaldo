using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private float delay = 1f;
    [SerializeField] private AudioSource soundEffect;
    
    [SerializeField] private bool damage = false;
    [SerializeField] private Collider2D damageCollider;
    
    private SpriteRenderer spriteRenderer;
    private DamageSource damageSource;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        damageSource = gameObject.GetComponent<DamageSource>();
    }


    public void Activate()
    {
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingLayerName = "Effects";
        Debug.Log("boom!");
        StartCoroutine("DestroyObject");
    }

    IEnumerator DestroyObject()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
        if(soundEffect != null)
        {
            soundEffect.Play();
        }
        if(damageSource != null)
        {
            damageSource.enabled = true;
        }
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
