using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class PlayerController : MonoBehaviour
{
    public bool controlsEnabled = true;

    public float walkSpeed;
    public float runSpeed;
    public Collider2D interactionPoint;
    public GameObject projectile1;
    public float projectileForce;
    public float attackDamage = 10;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private Facing facing = Facing.Down;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        SetInteractionPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if(controlsEnabled)
        {
            CheckInput();
            CheckForTrigger();
        }
    }

    private void CheckInput()
    {
        //get input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool running = Input.GetAxis("Fire3") > 0;

        //button events
        if(Input.GetButtonDown("Fire1"))
        {
            FireProjectile();
        }
        if(Input.GetButtonDown("Fire2"))
        {
            Attack();
        }
        if(Input.GetButtonDown("Fire3"))
        {
            InteractionCheck();
        }

        //set animation
        if(horizontalInput != 0 || verticalInput != 0)
        {
            SetInteractionPoint();
            DetermineFacing(horizontalInput, verticalInput);
            if(running)
            {animator.speed = 2;}
            SetWalkingAnim();
        }
        else
        {
            animator.speed = 1;
            SetIdleAnim();
        }

        //movement
        float speed = walkSpeed;
        if(running)
        {
            speed = runSpeed;
        }
        Vector2 newVelocity = new Vector2(horizontalInput * speed, verticalInput * speed);
        
        rb.velocity = newVelocity;
    }

    private void DetermineFacing(float horizontal, float vertical)
    {
        if(Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        { //horizontal
            if(horizontal > 0)
            { //right
                facing = Facing.Right;
            }
            else //left
            {
                facing = Facing.Left;
            }
        }
        else //vertical
        {
            if(vertical < 0)
            { //down
                facing = Facing.Down;
            }
            else
            { //up
                facing = Facing.Up;
            }
        }
    }

    private void SetIdleAnim()
    {
        switch(facing)
        {
            case Facing.Down:
                animator.Play("IdleDown");
                break;
            case Facing.Up:
                animator.Play("IdleUp");
                break;
            case Facing.Left:
                animator.Play("IdleLeft");
                break;
            case Facing.Right:
                animator.Play("IdleRight");
                break;
            default:
                break;
        }
    }

    private void SetWalkingAnim()
    {
        switch(facing)
        {
            case Facing.Down:
                animator.Play("WalkDown");
                break;
            case Facing.Up:
                animator.Play("WalkUp");
                break;
            case Facing.Left:
                animator.Play("WalkLeft");
                break;
            case Facing.Right:
                animator.Play("WalkRight");
                break;
            default:
                break;
        }
    }

    private void SetInteractionPoint()
    {
        switch(facing)
        {
            case Facing.Down:
                interactionPoint.transform.localPosition = new Vector3(0, -1, 0);
                break;
            case Facing.Up:
                interactionPoint.transform.localPosition = new Vector3(0, 1, 0);
                break;
            case Facing.Left:
                interactionPoint.transform.localPosition = new Vector3(-1, 0, 0);
                break;
            case Facing.Right:
                interactionPoint.transform.localPosition = new Vector3(1, 0, 0);
                break;
            default:
                break;
        }
    }

    //check for step triggers
    private void CheckForTrigger()
    {
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        if(boxCollider.OverlapCollider(filter, results) > 0)
        {
            Trigger target = results[0].gameObject.GetComponent<Trigger>();
            if(target != null)
            {
                target.Activate();
            }
        }
    }

    private void Attack()
    {
        List<Collider2D> results = new List<Collider2D>();
        if(interactionPoint.OverlapCollider(new ContactFilter2D(), results) > 0)
        {
            foreach(Collider2D current in results)
            {
                Enemy foundEnemy = current.gameObject.GetComponent<Enemy>();
                if(foundEnemy != null)
                {
                    Debug.Log("Attack hit " + foundEnemy.name);
                    //animator.Play("Attack"); //?
                    foundEnemy.TakeDamage(attackDamage);
                }
            }
        }
    }

    private void InteractionCheck()
    {
        List<Collider2D> results = new List<Collider2D>();
        if(interactionPoint.OverlapCollider(new ContactFilter2D(), results) > 0)
        {
            Interaction target = results[0].gameObject.GetComponent<Interaction>();
            if(target != null)
            {
                target.Activate();
            };
        }
    }

    private void FireProjectile()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        GameObject newProjectile = Instantiate(projectile1, position, Quaternion.identity);
        switch(facing)
        {
            case Facing.Up:
                newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * projectileForce, ForceMode2D.Impulse);
                break;
            case Facing.Down:
                newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.down * projectileForce, ForceMode2D.Impulse);
                break;
            case Facing.Left:
                newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.left * projectileForce, ForceMode2D.Impulse);
                break;
            case Facing.Right:
                newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * projectileForce, ForceMode2D.Impulse);
                break;
        }
    }
}

public enum Facing
{
    Up,
    Down,
    Left,
    Right
}
