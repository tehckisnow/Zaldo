using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    //[SerializeField] private float knockbackStrength = 2;

    private Rigidbody2D rb;
    
    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
    //     if(rb != null)
    //     {
    //         Vector2 direction = collision.transform.position - transform.position;
    //         rb.AddForce(direction.normalized * knockbackStrength, ForceMode2D.Impulse);
    //     }
    // }

    public void TakeKnockback(Vector3 source, float power)
    {
        Debug.Log(this.name);
        Vector2 direction = transform.position - source;
        rb.AddForce(direction.normalized * power, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
