using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{


    public HealthBar healthBar;
    public PlayerMovement player;
    public Text hpText;

    public int bootsSpeed;
    public int jumpWings;
    public int atkSpeedItem;
    public int defItem;
    public int atkItem;
    public int hpItem;


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
        
        hpText.text ="HP : " + playerCurrentHP.ToString() + " / " + playerHPMax.ToString();
        
        player.myAnimation.SetFloat("AttackSpeed",1+(0.1f*atkSpeedItem));

        playerDef = 5*defItem;
        playerAtk = 10 + (atkItem * 5);
        playerHPMax = 100 + (10*hpItem);


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



    public void GetDamaged(float enemyDmg)
    {
        playerCurrentHP -= enemyDmg-playerDef;
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
