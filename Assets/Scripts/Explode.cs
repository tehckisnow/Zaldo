using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private float delay = 1f;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();    
    }


    public void Activate()
    {
        spriteRenderer.sprite = sprite;
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
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
