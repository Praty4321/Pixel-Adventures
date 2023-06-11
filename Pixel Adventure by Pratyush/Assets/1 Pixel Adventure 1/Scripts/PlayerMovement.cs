using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb; //used for moving the player left and right
    private Animator anim; // only used for animations
    private SpriteRenderer sprite;//used for flipping the player on x axis
    private BoxCollider2D coll; // used for collecting coins, death, etc

    [SerializeField] private LayerMask jumpableGround; // used for player to jump from moving platform

    private float dirX = 0f;//for movement and animation togeather
    [SerializeField] private float moveSpeed = 7f;//better use serializefield than making it public 
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling}//here we know that numbering will be {0,1,2,3} respectively and we will use this numbering in unity Animator to for int state parameter.
    [SerializeField] private AudioSource JumpSoundEffect = null;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal"); //getAxisRaw will give interger and getAxis() will give float value
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            JumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationstate();//using seperate functions make code organized.
        
        //extra : 
        

    }

    private void UpdateAnimationstate()
    {
        MovementState state;

        if (dirX > 0f)//0f gives us precision.
        {
            state = MovementState.running; // running is the exact spelling of boolean variable i made in the animator panel
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state=MovementState.idle;   
        }

        if(rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround );
    }



} //class
