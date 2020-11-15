using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerControls playerControls;
    float movementInput;
    float crouchInput;
    [SerializeField] private float speed, jumpSpeed, crouchSpeed;
    [SerializeField] private LayerMask ground;
    public Collider2D crouchCol;
    private Rigidbody2D rb;
    private Collider2D col;
    bool MustCrouch = false;
    public Animator animator;
    bool facingRight = true;

    //Shooting
    //public GameObject fireball;
    //public Transform firepoint;
    public bool shoot;


    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerControls.Land.Jump.performed+= _ => Jump();
        playerControls.Land.Shoot.performed += _ => shoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Read the movement Input
        movementInput = playerControls.Land.Move.ReadValue<float>();
        crouchInput = playerControls.Land.Crouch.ReadValue<float>();
        //FirstMovement();

        

        animator.SetFloat("speed", Mathf.Abs(movementInput));
        animator.SetBool("crouch", crouchCol.enabled);
        animator.SetBool("grounded", IsGrounded());
    }
    private void FixedUpdate()
    {
        FirstMovement();
        //secondmovement();
    }

    private void Jump()
    {
        
        if (IsGrounded())
        {        
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        Vector2 bottomLeft = col.bounds.center;
        bottomLeft.x -= col.bounds.extents.x - 0.05f;
        bottomLeft.y -= col.bounds.extents.y;

        Vector2 bottomRight = col.bounds.center;
        bottomRight.x += col.bounds.extents.x - 0.05f;
        bottomRight.y -= col.bounds.extents.y;

 
        return Physics2D.OverlapArea(bottomLeft, bottomRight, ground);
    }

    private void FirstMovement()
    {
        //crouch movement
        if (crouchInput == 1 || MustCrouch )
        {
            if (CanStandUp())
            {
                MustCrouch = false;
            }
            else
            {
                MustCrouch = true;
            }
            
            Vector3 currentPostion = transform.position;
            currentPostion.x += movementInput * crouchSpeed * Time.deltaTime;
            transform.position = currentPostion;
            crouchCol.enabled = false;
        }
        //normal Movement
        else 
        {
            Vector3 currentPostion = transform.position;
            currentPostion.x += movementInput * speed * Time.deltaTime;
            transform.position = currentPostion;
            crouchCol.enabled = true;
        }

        if (movementInput < 0 && facingRight)
        {
            Flip();
        }
        else if (movementInput > 0 && !facingRight)
        {
            Flip();
        }

    }

    private bool CanStandUp()
    {
        Vector2 leftShoulder = col.bounds.center;
        leftShoulder.x -= col.bounds.extents.x + 0.01f;
        leftShoulder.y += col.bounds.extents.y - 0.01f;

        Vector2 rightShoulder = col.bounds.center;
        rightShoulder.x += col.bounds.extents.x - 0.01f;
        rightShoulder.y += col.bounds.extents.y - 0.01f;
        return !Physics2D.OverlapArea(leftShoulder, rightShoulder, ground);

    }
    

    private void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
        
        
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;
    }

  
    //private void Shoot()
    //{
    //    Instantiate(fireball, firepoint.position, firepoint.rotation);
    //}




    //private void SecondMovement()
    //{
    //    //crouch movement
    //    if (crouchInput == 1)
    //    {
    //        float move = movementInput * crouchSpeed * Time.deltaTime;
    //        crouchCol.enabled = false;
    //    }
    //    //normal Movement
    //    else
    //    {
    //        float move = movementInput * speed * Time.deltaTime;
    //        crouchCol.enabled = true;
    //    }
    //}
}
