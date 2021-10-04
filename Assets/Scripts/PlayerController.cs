using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System;

public class PlayerController : MonoBehaviour
{
    public bool controlsEnabled = true;

    public float walkSpeed;
    public float runSpeed;
    public Collider2D interactionPoint;
    public Collider2D swordCollider;
    public GameObject pivotArm; //used to hold interaction point and swordcollider
    public GameObject projectile1;
    public GameObject bomb1;
    public AudioSource layBombSfx;
    public float projectileForce;
    public int attackDamage = 2;
    public AudioSource projectileSound;
    public AudioSource swordSound;

    public Dictionary<ObtainableTypes, int> obtainables = new Dictionary<ObtainableTypes, int>();

    public InputMode inputMode = InputMode.Normal;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator animator;
    public Facing facing = Facing.Down;
    private bool readyForArrow = true; //projectile is ready
    private bool readyForBomb = true; //bomb is ready
    private float bombReadyDelay = 1f;

    private float horizontal;
    private float vertical;

    private Vector2 movement;
    public GameObject carriedItem = null;
    private Health playerHealth = null;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.player != this.gameObject)
        {
            if(GameManager.instance.player == null)
            {
                GameManager.instance.player = this.gameObject; //!
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        
        DontDestroyOnLoad(this.gameObject);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerHealth = GetComponent<Health>();
        SetInteractionPoint();

        foreach(ObtainableTypes thing in Enum.GetValues(typeof(ObtainableTypes)))
        {
            obtainables.Add(thing, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(controlsEnabled)
        {
            CheckInput();
            CheckForTrigger();
            UpdateHUD();
        }
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void ApplyMovement()
    {
        rb.AddForce(movement);
        movement = new Vector2(0f, 0f);
    }

    public void HaltMovement()
    {
        rb.velocity = new Vector2(0f, 0f);
        Debug.Log(rb.velocity);
    }

    private void UpdateHUD()
    {
        HUDManager hud = GameManager.instance.hud.GetComponent<HUDManager>();
        hud.SetValues(
            obtainables[ObtainableTypes.Rupees], 
            obtainables[ObtainableTypes.Bombs], 
            obtainables[ObtainableTypes.Arrows],
            obtainables[ObtainableTypes.Keys]);
        
        int maxHealth = playerHealth.GetMaxHealth();
        int health = playerHealth.GetHealth();
        hud.SetHearts(health, maxHealth);
    }

    private void CheckInput()
    {
        switch(inputMode)
        {
            case InputMode.Normal:
                NormalInput();
                break;
            case InputMode.Text:
                TextInput();
                break;
            case InputMode.Carry:
                CarryInput();
                break;
            case InputMode.Wait:
            default:
                break;
        }
    }

    private void TextInput()
    {
        if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3"))
        {
            GameManager.instance.textbox.Advance();
        }
    }

    private void CarryInput()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            carriedItem?.GetComponent<Liftable>()?.ThrowObject(facing);
        }
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        //set animation
        if(horizontalInput != 0 || verticalInput != 0)
        {
            horizontal = horizontalInput;
            vertical = verticalInput;
            animator.SetBool("Moving", true);
            SetInteractionPoint();
            DetermineFacing(horizontalInput, verticalInput);
        }
        else
        {
            animator.SetBool("Moving", false);
            animator.speed = 1;
            //SetIdleAnim();
        }
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);

        //movement
        float speed = walkSpeed;
        
        Vector2 newVelocity = new Vector2(horizontalInput * speed, verticalInput * speed);
        
        movement = newVelocity;
    }

    private void NormalInput()
    {
        //get input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool running = Input.GetAxisRaw("Fire3") > 0;

        //button events
        if(Input.GetButtonDown("Fire1"))
        {
            if(readyForArrow)
            {
                StartCoroutine(FireProjectile());                
            }
        }
        if(Input.GetButtonDown("Jump"))
        {
            Attack();
        }
        if(Input.GetButtonDown("Fire2"))
        {
            //DescriptionCheck();
            InteractionCheck();
        }
        //bomb
        if(Input.GetButtonDown("Fire4"))
        {
            DropBomb();
        }


        //set animation
        if(horizontalInput != 0 || verticalInput != 0)
        {
            horizontal = horizontalInput;
            vertical = verticalInput;
            animator.SetBool("Moving", true);
            SetInteractionPoint();
            DetermineFacing(horizontalInput, verticalInput);
            if(running)
            {animator.speed = 2;}
            //SetWalkingAnim();
        }
        else
        {
            animator.SetBool("Moving", false);
            animator.speed = 1;
            //SetIdleAnim();
        }
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);

        //movement
        float speed = walkSpeed;
        if(running)
        {
            speed = runSpeed;
        }
        Vector2 newVelocity = new Vector2(horizontalInput * speed, verticalInput * speed);
        
        movement = newVelocity;
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

    private void DropBomb()
    {
        if(obtainables[ObtainableTypes.Bombs] > 0 && readyForBomb)
        {
            readyForBomb = false;
            //controlsEnabled = false;
            //yield return new WaitForSeconds(0.5f);
            //projectileSound.Play();
            
            obtainables[ObtainableTypes.Bombs]--;
            
            //facing
            Vector3 position = interactionPoint.transform.position;

            //place bomb
            Instantiate(bomb1, position, Quaternion.identity);
            if(layBombSfx != null)
            {
                layBombSfx.Play();
            }
            StartCoroutine(ReadyBomb());
        }
    }

    IEnumerator ReadyBomb()
    {
        yield return new WaitForSeconds(bombReadyDelay);
        readyForBomb = true;
    }
    
    private void SetInteractionPoint()
    {
        switch(facing)
        {
            case Facing.Down:
                pivotArm.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case Facing.Up:
                pivotArm.transform.eulerAngles = new Vector3(0, 0, 180);
                break;
            case Facing.Left:
                pivotArm.transform.eulerAngles = new Vector3(0, 0, -90);
                break;
            case Facing.Right:
                pivotArm.transform.eulerAngles = new Vector3(0, 0, 90);
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
            foreach(Collider2D item in results)
            {
                Trigger target = item.gameObject.GetComponent<Trigger>();
                if(target != null)
                {
                    target.Activate();
                }
            }
        }
    }

    private void Attack()
    {
        swordSound.Play();
        animator.SetTrigger("Attack");
        swordCollider.gameObject.SetActive(true);
        StartCoroutine(EndAttack());
    }
    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(0.2f);
        swordCollider.gameObject.SetActive(false);
    }

    // private void DescriptionCheck()
    // {
    //     List<Collider2D> results = new List<Collider2D>();
    //     if(interactionPoint.OverlapCollider(new ContactFilter2D(), results) > 0)
    //     {
    //         //!Following line uses first result instead of iterating, see InteractionCheck() below
    //         foreach(Collider2D item in results)
    //         {
    //             Description target = item.gameObject.GetComponent<Description>();
    //             if(target != null)
    //             {
    //                 //callback  //!
    //                 //disable input //!
    //                 inputMode = InputMode.Text;
    //                 GameManager.instance.textbox.Open(target.description);
    //                 break;
    //             };
    //         }
    //     }
    // }


    private void InteractionCheck()
    {
        List<Collider2D> results = new List<Collider2D>();
        if(interactionPoint.OverlapCollider(new ContactFilter2D(), results) > 0)
        {
            foreach(Collider2D item in results)
            {

                Interaction target = item.gameObject.GetComponent<Interaction>();
                if(target != null)
                {
                    target.Activate();
                    break;
                };
            }
        }
    }

    private IEnumerator FireProjectile()
    {
        if(obtainables[ObtainableTypes.Arrows] > 0)
        {
            readyForArrow = false;
            controlsEnabled = false;
            yield return new WaitForSeconds(0.5f);
            //arrows--;
            obtainables[ObtainableTypes.Arrows]--;
            
            projectileSound.Play();
            
            //Vector2 position = new Vector2(transform.position.x, transform.position.y);
            //! replace position with interaction point position so player wont shoot himself
            Vector2 position = interactionPoint.transform.position;
            
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
            controlsEnabled = true;
            readyForArrow = true;
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

public enum InputMode
{
    Normal,
    Text,
    Carry,
    Wait
}
