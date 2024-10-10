using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PauseMenu pauseMenu;


    public Animator myAnimation;
    private bool isCrouching;
    public bool isBasicAttack;
    public Transform hitbox;
    public GameObject playerGFX;


    [Header("Stats")]
    public Rigidbody2D playerRb;
    public float baseSpeed;
    public float input;
    public SpriteRenderer playerSprite;
    //public float speed;
    public Transform lookDirection;

    [Header("Jump")]
    //public float jumpForce;
    public LayerMask groundLayer;
    public LayerMask groundLayerDrop;
    public LayerMask enemyLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;

    private bool isJumping;
    private bool canJump;
    private bool isFalling;
    public float jumpHoldForce = 7f;
    public float jumpHoldDuration = 0.2f;
    public float maxJumpVelocity = 10f;
    public float airFriction = 0.5f;
    //public LayerMask groundLayer;

    //private Rigidbody2D playerRb;
    //private bool isGrounded;
    private bool jumpPressed;
    private float jumpTime;



    [Header("WallClimb")]
    public LayerMask wallLayer;
    private bool isWalled;
    public Transform lArmPosition;
    public Transform rArmPosition;
    public float wallCheckCircle;
    public Transform rwallTop;
    public Transform rwallBot;
    public Transform lwallTop;
    public Transform lwallBot;


    [Header("Dash")]
    public float dashIntensity;
    public float dashCooldown;
    private bool canDash = true;
    private Vector2 dashDirection;
    public float dashTimer;
    public bool isDashing;


    public PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory.playerSpeed = baseSpeed;
        canDash = true;
        myAnimation = playerGFX.GetComponent<Animator>();
        playerSprite=playerGFX.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
        {
        if (!pauseMenu.isPaused)
        {
            /**************************** ANIMATIONS **********************************/
            if (isJumping) { myAnimation.SetBool("Jump", true); } else { myAnimation.SetBool("Jump", false); }
            if (isFalling) { myAnimation.SetBool("Falling", true); } else { myAnimation.SetBool("Falling", false); }
            if (isDashing) { myAnimation.SetBool("Dash", true); } else { myAnimation.SetBool("Dash", false); }
            if (isCrouching) { myAnimation.SetBool("Crouch", true); } else { myAnimation.SetBool("Crouch", false); }
            if (!isDashing)
            {
                if (isBasicAttack)
                {
                    myAnimation.SetBool("BasicAttack", true);
                }
                else { myAnimation.SetBool("BasicAttack", false); }

            }
            if (isWalled) { myAnimation.SetBool("WallClimb", true); } else { myAnimation.SetBool("WallClimb", false); }

            /**************************************************************************/


            if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl))
            {
                isCrouching = true;

            }
            else { isCrouching = false; }


            if (isDashing) { return; }




            playerInventory = GetComponent<PlayerInventory>();
            

            // Left Right Movement flip sprite and change look direction
            input = Input.GetAxisRaw("Horizontal");
            if (input < 0 && !isBasicAttack)
            {
                myAnimation.SetBool("Run", true);
                //playerSprite.flipX = true;
                if (lookDirection.localPosition.x > 0)
                {
                    lookDirection.localPosition = new Vector3(-0.2f, 0, 0);
                    Vector3 lTemp = playerGFX.transform.localScale;
                    lTemp.x *= -1;
                    playerGFX.transform.localScale = lTemp;
                    //hitbox.localScale *= -1f;

                }

            }
            else if (input > 0 && !isBasicAttack)
            {
                //playerSprite.flipX = false;
                if (lookDirection.localPosition.x < 0)
                {
                    lookDirection.localPosition = new Vector3(0.2f, 0, 0);
                    Vector3 lTemp = playerGFX.transform.localScale;
                    lTemp.x *= -1;
                    playerGFX.transform.localScale = lTemp;
                    //hitbox.localScale *= -1f;
                    //hitbox.localPosition = new Vector3(-1 * hitbox.localPosition.x, -1 * hitbox.localPosition.y, -1 * hitbox.localPosition.z);
                    //hitbox.transform.localPosition.Scale(new Vector3(-1f, -1f, -1f));

                    //hitbox.eulerAngles = new Vector3(0, 0, 0);
                }
                myAnimation.SetBool("Run", true);
            }
            else { myAnimation.SetBool("Run", false); }

            //***************************************************************************** Jump 
            canJump = isGrounded || isWalled;
            isFalling = playerRb.velocity.y < (-(playerRb.gravityScale / 2));
            jumpHoldDuration = (0.2f + (0.025f * playerInventory.jetPackItem));
            maxJumpVelocity = 10f + (0.2f*playerInventory.jumpItem);


            isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer) ||
            Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayerDrop) ||
            Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, enemyLayer);


            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                jumpPressed = true;
                jumpTime = Time.time;
                isJumping = true;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpPressed = false;
            }

            /*
            if (canJump && Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                //myAnimation.SetBool("Jump", true);
                jumpTimeCounter = jumpTimer;
                playerRb.AddForce(Vector2.up * playerInventory.playerJumpForce);
                //playerRb.velocity = Vector2.up * playerInventory.playerJumpForce;

            }

            if (Input.GetButton("Jump") && isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    playerRb.AddForce(Vector2.up * playerInventory.playerJumpForce);
                    //playerRb.velocity = Vector2.up * playerInventory.playerJumpForce;
                    jumpTimeCounter -= Time.deltaTime;


                }
                else
                {
                    isJumping = false;

                }
            }
            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;

            }

            */
            /**********************************************************************************************/

            // Walljump

            if ((Physics2D.OverlapArea(rwallTop.position, rwallBot.position, wallLayer) ||
                Physics2D.OverlapArea(lwallTop.position, lwallBot.position, wallLayer) ||
                Physics2D.OverlapArea(rwallTop.position, rwallBot.position, groundLayer) ||
                Physics2D.OverlapArea(lwallTop.position, lwallBot.position, groundLayer)) && !isGrounded && !isJumping)
            {
                isWalled = true;
                //jumpTimeCounter = jumpTimer;
                playerRb.velocity = Vector2.down * (playerRb.gravityScale / 2.5f);
            }
            else
            {
                isWalled = false;
            }
            /*
             * 
             */
            /******************************************* Dash Direction ********************************/
            if (lookDirection.localPosition.x > 0)
            {
                dashDirection = Vector2.right;
            }
            else if (lookDirection.localPosition.x < 0)
            {
                dashDirection = Vector2.left;
            }
            if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && canDash)
            {
                StartCoroutine(Dash());
            }

        }
        /********************************** **************************/


    }
    void FixedUpdate()
    {
        //if (isDashing) { print("caca"); }
        if (isDashing) { return; }

        //jump
        if (jumpPressed && (Time.time - jumpTime) < jumpHoldDuration)
        {
            playerRb.AddForce(Vector2.up * (playerInventory.playerJumpForce - 3), ForceMode2D.Impulse);
        }
        else if (canJump && jumpPressed && isJumping)
        {
            playerRb.AddForce(Vector2.up * playerInventory.playerJumpForce, ForceMode2D.Impulse);
        }

        // Limiter la vitesse verticale maximale
        if (playerRb.velocity.y > maxJumpVelocity)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, maxJumpVelocity);
        }

        // Appliquer une friction aérienne
        if (!isGrounded)
        {
            playerRb.velocity -= new Vector2(0f, airFriction * Time.deltaTime);
        }
        // Réinitialiser l'état de saut si le joueur est revenu au sol
        if (isGrounded && isJumping)
        {
            isJumping = false;
        }

        //crouch
        if (!isCrouching)
        {
            playerInventory.playerSpeed = (baseSpeed + (0.05f * playerInventory.bootsSpeed) * baseSpeed);
            
        }
        else { playerInventory.playerSpeed = ((1 / 2f * baseSpeed) + (0.02f * playerInventory.bootsSpeed) * (1 / 4f * baseSpeed));
            
        }
        playerRb.velocity = new Vector2((input * playerInventory.playerSpeed), playerRb.velocity.y);
        
        


    }
    private IEnumerator Dash()
    {
        dashIntensity = 2.4f * (baseSpeed + (0.05f * playerInventory.bootsSpeed) * baseSpeed);

        canDash = false;
        isDashing = true;

        if (!isBasicAttack || (isBasicAttack && input==0))
        {
            playerRb.velocity = dashDirection * dashIntensity;
        }
        else { playerRb.velocity =  new Vector2(input,0) * dashIntensity; }


        yield return new WaitForSeconds(dashTimer);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;


        /*
        canDash = dashCooldownTimer <= 0;
        canDash = false;
        dashCooldownTimer = dashCooldown;


        if (lookDirection.localPosition.x > 0)
        {
            dashDirection = Vector2.right;
            //playerRb.velocity = Vector2.right * dashIntensity;
        }
        else if (lookDirection.localPosition.x < 0)
        {
            dashDirection = Vector2.left;
            //playerRb.velocity = Vector2.left * dashIntensity;
        }
        */
    }


}


