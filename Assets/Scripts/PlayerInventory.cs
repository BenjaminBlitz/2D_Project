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
    public int jumpItem;
    public int atkSpeedItem;
    public int defItem;
    public int atkItem;
    public int hpItem;
    public int critItem;
    public int doubleHPItem;
    public int critHealItem;


    [Header("Stats")]
    public float playerSpeed, playerJumpForce,playerCurrentHP, playerHPMax, playerDef, playerAtk, playerAtkSpeed, playerCrit, lifeStealItem, doubleDropItem, jetPackItem;


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


        playerJumpForce = 10 + (0.2f * jumpItem);
        playerDef = 5*defItem;
        playerAtk = 10 + (atkItem * 5);
        playerHPMax = (100 + (10*hpItem)) * (1+doubleHPItem);
        playerCrit = 0 + (10*critItem);

        if (Input.GetKeyDown(KeyCode.F))
        {
            bootsSpeed++;
            print(playerSpeed);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            jumpItem++;
            print(playerJumpForce);
        }
        

    }



    public void GetDamaged(float enemyDmg)
    {
        playerCurrentHP -= enemyDmg-playerDef;
        healthBar.UpdateHealthBar(playerCurrentHP, playerHPMax);
    }


    public void LifeSteal(bool crit)
    {
        if (crit)
        {
            if (playerCurrentHP < playerHPMax)
            {
                if (playerCurrentHP + lifeStealItem + (critHealItem * 5) > playerHPMax)
                {
                    playerCurrentHP = playerHPMax;
                    healthBar.UpdateHealthBar(playerCurrentHP, playerHPMax);
                }
                else
                {
                    playerCurrentHP += lifeStealItem;
                    playerCurrentHP += critHealItem * 5;
                    healthBar.UpdateHealthBar(playerCurrentHP, playerHPMax);
                }

            }
        }
        else
        {
            if (playerCurrentHP < playerHPMax)
            {
                if (playerCurrentHP + lifeStealItem > playerHPMax)
                {
                    playerCurrentHP = playerHPMax;
                    healthBar.UpdateHealthBar(playerCurrentHP, playerHPMax);
                }
                else
                {
                    playerCurrentHP += lifeStealItem;
                    healthBar.UpdateHealthBar(playerCurrentHP, playerHPMax);
                }

            }
        }
    }

    public void HpUp(int value)
    {
        if (value == 1)
        {
            hpItem++;
            playerHPMax = (100 + (10 * hpItem)) * (1 + doubleHPItem);
            playerCurrentHP += 10;
            healthBar.UpdateHealthBar(playerCurrentHP, playerHPMax);
        }
        if (value == 2){
            doubleHPItem++;
            playerHPMax = (100 + (10 * hpItem)) * (1 + doubleHPItem);
            playerCurrentHP =playerHPMax;
            healthBar.UpdateHealthBar(playerCurrentHP, playerHPMax);
        }
    }



}
