using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class JumpKingScript : MonoBehaviour, IDataPersistence
{
    // Public variables for inspector access
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sprite;
    public LayerMask groundMask;
    public LayerMask wallMask;
    public PhysicsMaterial2D BounceMat, NormalMat;
    private Vector2 startingPosition;

    private float startTime;

    private float timePlayed;
    // Variables for character movement
    public float moveSpeed = 7f; // Character movement speed
    public float moveInput; // Input for character movement

    // Variables for character state
    public bool isGrounded; // Check if the character is on the ground
    public bool isCollidingWithWall; // Check if the character is colliding with a wall
    public bool canJump = true; // Check if the character can jump
    public float jumpValue = 0.0f; // Jump power

    private void Start()
    {
        Debug.Log("JumpKingScript Start");
        // Get necessary components at the start
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void onResume()
    {
        startTime = Time.deltaTime;
    }

    private void onPause()
    {
        DataPersistenceManager.instance.SaveGame();
    }

    private void Update()
    {
        timePlayed += Time.deltaTime;
        moveInput = Input.GetAxis("Horizontal");

        HandleHorizontalMovement();

        // Check if the character is grounded
        isGrounded = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - 0.5f),
            new Vector2(1.2f, 1.2f), 0f, groundMask);

        // Check if the character is colliding with a wall
        isCollidingWithWall = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y),
      new Vector2(1.5f, 0.9f), 0f, wallMask);

        HandleMaterialAndJumpInput();

        UpdateAnimation();
    }

    private void HandleHorizontalMovement()
    {
        if (jumpValue == 0.0f && isGrounded)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
    }

    private void HandleMaterialAndJumpInput()
    {
        // Apply bounce material if not grounded, normal material if grounded
        if (!isGrounded)
        {
            rb.sharedMaterial = BounceMat;
            canJump = false;
        }
        else
        {
            anim.SetBool("Collide", false);
            rb.sharedMaterial = NormalMat;
            canJump = true;
        }

        // Handle jumping input
        if (Input.GetKey(KeyCode.Space) && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
            jumpValue += 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            jumpValue = 15f;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }

        // Cap the jump power
        if (jumpValue >= 40f && isGrounded)
        {
            jumpValue = 40f;
        }

        // Handle releasing the jump key
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isGrounded)
            {
                moveSpeed = 15f;
                rb.velocity = new Vector2(moveInput * moveSpeed, jumpValue);
            }
            jumpValue = 0f;
        }

        // Reset the movement speed
        if (!canJump)
        {
            moveSpeed = 7f;
        }
    }

    // Function to update character animations
    void UpdateAnimation()
    {
        // Reset all animation states
        anim.SetBool("Running", false);
        anim.SetBool("Charging", false);
        anim.SetBool("Jumping", false);
        anim.SetBool("Falling", false);
        // Handle animation states based on input and character state
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded || Input.GetKey(KeyCode.Space) && isGrounded
            || Input.GetKeyDown(KeyCode.Space) && isGrounded && isCollidingWithWall || Input.GetKey(KeyCode.Space) && isGrounded && isCollidingWithWall)
        {
            anim.SetBool("Charging", true);
        }
        else if (isCollidingWithWall && !isGrounded)
        {
            anim.SetBool("Collide", true);
        }
        else if (rb.velocity.y > 0)
        {
            anim.SetBool("Jumping", true);
        }
        else if (rb.velocity.y < -0.5)
        {
            anim.SetBool("Falling", true);
        }
        else if (moveInput > 0.0f || moveInput < 0.0f)
        {

            anim.SetBool("Running", true);
            sprite.flipX = moveInput < 0.0f; // Flip sprite when moving left
        }
    }

    public void GetPosition()
    {
        Debug.Log(this.transform.position);
    }
    public void LoadData(GameData data)
    {
        this.timePlayed = data.totalTimePlayed;
        this.transform.position = data.playerPosition;
    }
    public void SaveData(ref GameData data)
    {
        Debug.Log("saving player...");
        data.totalTimePlayed = timePlayed;
        data.playerPosition = this.transform.position;
    }

    private void OnDrawGizmos()
    {
        DrawOverlapBox();
        DrawOverlapBox1();
    }

    private void OnDrawGizmosSelected()
    {
        DrawOverlapBox();
        DrawOverlapBox1();
    }

    private void DrawOverlapBox()
    {
        Gizmos.color = Color.yellow;

        // Calculate the box position and draw the box
        Vector2 boxCenter = new Vector2(transform.position.x, transform.position.y);
        Gizmos.DrawWireCube(boxCenter, new Vector3(1.5f, 0.9f, 0f));
    }

    private void DrawOverlapBox1()
    {
        Gizmos.color = Color.green;

        // Calculate the box position and draw the box
        Vector2 boxCenter = new Vector2(transform.position.x + (moveInput >= 0 ? 0.6f : -0.6f), transform.position.y);
        Gizmos.DrawWireCube(boxCenter, new Vector3(0.8f, 0.9f, 0f));
    }

}