using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StepTrigger : Trigger
{
    //public bool active = true;

    [SerializeField] private Sprite defaultSprite = null;
    [SerializeField] private Sprite activatedSprite = null;
    [SerializeField] private UnityEvent action = null;
    [SerializeField] private AudioSource sfx = null;

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
