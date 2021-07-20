using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liftable : Interaction
{
    public int dropChanceOutOf100 = 50;
    public List<GameObject> drops;

    private GameObject playerObject;
    private PlayerController playerComponent;
    private float reactivateDelay = 0.2f;
    [SerializeField] private float projectileLife = 1f;

    public override void Activate()
    {
        playerObject = GameManager.instance.player;
        playerComponent = playerObject.GetComponent<PlayerController>();
        playerComponent.carriedItem = gameObject;
        gameObject.transform.parent = playerObject.transform;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        playerComponent.inputMode = InputMode.Carry;
        playerObject.GetComponent<Animator>().SetBool("Carrying", true);
        if(drops.Count > 0)
        {
            DropItem();
        }
        StartCoroutine("LiftObject");
        //!player animation

    }

    private void DropItem()
    {
        if(Random.Range(1, 100) <= dropChanceOutOf100)
        {
            int choice = Random.Range(0, drops.Count);
            Instantiate(drops[choice], gameObject.transform.position, Quaternion.identity);
        }
    }

    IEnumerator LiftObject()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        float maxDistanceDelta = 0.3f;
        float seconds = 0.1f;
        //string origSortingLayer = spriteRenderer.sortingLayerName;
        spriteRenderer.sortingLayerName = "Effects";
        Vector2 target = new Vector2(0, 1);
        Vector2 current = gameObject.transform.localPosition;
        while(current != target)
        {
            current = gameObject.transform.localPosition;
            gameObject.transform.localPosition = Vector2.MoveTowards(current, target, maxDistanceDelta);
            yield return new WaitForSeconds(seconds);
        }
    }

    public void ThrowObject(Facing facing)
    {
        gameObject.transform.parent = null;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        playerObject.GetComponent<Animator>().SetBool("Carrying", false);
        //activate RigidBody
        rb.isKinematic = false;
        //apply throwing force based on player facing
        float x = 0;
        float y = 0;
        switch(facing)
        {
            case Facing.Up:
                y = 1; //!
                break;
            case Facing.Down:
                y = -1; //!
                break;
            case Facing.Left:
                x = -1;
                break;
            case Facing.Right:
                x = 1;
                break;
            default:
                break;
        }
        rb.AddForce(new Vector2(x, y) * 500);
        ////apply gravity force

        //activate damage component
        //gameObject.GetComponent<DamageSource>().enabled = true;
        //gameObject.GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine("ActivateDamage");

        playerComponent.inputMode = InputMode.Normal;
        playerComponent.carriedItem = null;
        //start timer ??
        //??
        StartCoroutine(DestroyTimer());
    }

    IEnumerator ActivateDamage()
    {
        yield return new WaitForSeconds(reactivateDelay);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<DamageSource>().enabled = true;
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(projectileLife);
        gameObject.GetComponent<Explode>().Activate();
    }
}
