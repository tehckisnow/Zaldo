using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StepTrigger : Trigger
{
    //public bool active = true;

    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite activatedSprite;
    [SerializeField] private UnityEvent action;
    [SerializeField] private AudioSource sfx;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Activate()
    {
        if(active)
        {
            active = false;
            spriteRenderer.sprite = activatedSprite;
            if(sfx != null)
            {
                sfx.Play();
            }
            action.Invoke();
        }
    }

    public void Deactivate()
    {
        spriteRenderer.sprite = defaultSprite;
    }
}
