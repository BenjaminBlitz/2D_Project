using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{


    public HealthBar healthBar;
    public PlayerMovement player;

    public int bootsSpeed;
    public int jumpWings;
    public int atkSpeedItem;


    [Header("Stats")]
    public float playerSpeed, playerJumpForce,playerCurrentHP, playerHPMax, playerDef, playerAtk, playerAtkSpeed;


    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHP = playerHPMax;
        healthBar =GetComponentInChildren<HealthBar>();
        healthBar.UpdateHealthBar(playerCurrentHP, playerHPMax);
        

    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        player.myAnimation.SetFloat("AttackSpeed",1+(0.1f*atkSpeedItem));




        if (Input.GetKeyDown(KeyCode.F))
        {
            bootsSpeed++;
            print(playerSpeed);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            jumpWings++;
            print(playerJumpForce);
        }


    }



    public void GetDamaged()
    {
        playerCurrentHP -= 10;
        healthBar.UpdateHealthBar(playerCurrentHP, playerHPMax);
    }


    /*
        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (gameObject.GetComponentInChildren<Collider2D>().GetType() == typeof(CapsuleCollider2D)){
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    playerCurrentHP -= 10;
                    healthBar.UpdateHealthBar(playerCurrentHP, playerHPMax);
                }
            }


        }

        *
        */


}
