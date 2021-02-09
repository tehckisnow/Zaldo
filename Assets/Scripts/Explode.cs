using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour, IExecutableAction
{
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExecuteAction()
    {
        Debug.Log("boom!");
        spriteRenderer.color = new Color(0.3f, 0.3f, 0.4f, 0.3f);
    }
}
