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
    public float jumpForce;
    public LayerMask groundLayer;
    public LayerMask groundLayerDrop;
    public LayerMask enemyLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;
    public float jumpTimer = 0.2f;
    public float jumpTimeCounter;
    private bool isJumping;
    private bool canJump;
    private bool isFalling;



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
            jumpTimer = (0.2f + (0.025f * playerInventory.jumpWings));

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

            // Jump 
            canJump = isGrounded || isWalled;
            isFalling = playerRb.velocity.y < (-(playerRb.gravityScale / 2));



            isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer) ||
            Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayerDrop) ||
            Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, enemyLayer);

            if (canJump && Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                //myAnimation.SetBool("Jump", true);
                jumpTimeCounter = jumpTimer;
                playerRb.velocity = Vector2.up * playerInventory.playerJumpForce;

            }

            if (Input.GetButton("Jump") && isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    playerRb.velocity = Vector2.up * playerInventory.playerJumpForce;
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


            /**********************************************************************************************/

            // Walljump

            if ((Physics2D.OverlapArea(rwallTop.position, rwallBot.position, wallLayer) ||
                Physics2D.OverlapArea(lwallTop.position, lwallBot.position, wallLayer) ||
                Physics2D.OverlapArea(rwallTop.position, rwallBot.position, groundLayer) ||
                Physics2D.OverlapArea(lwallTop.position, lwallBot.position, groundLayer)) && !isGrounded && !isJumping)
            {
                isWalled = true;
                jumpTimeCounter = jumpTimer;
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


