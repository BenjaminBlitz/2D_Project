using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public PlayerInventory playerStats;
    public GameObject damagePopUp;

    [Header("Stats")]
    public float enemyHP;
    public float enemyMaxHP;
    public float enemyArmor;
    public float enemyDamage;
    public float enemyAtkSpeed;
    public float enemySpeed;
    public HealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        enemyHP = enemyMaxHP;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.UpdateHealthBar(enemyHP, enemyMaxHP);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHP <= 0)
        {
            gameObject.GetComponent<EnemyDropitem>().DropItem(playerStats.doubleDropItem);
            Destroy(gameObject);
        }
    }

    public void Hitted(float value,bool crit)
    {
        float damageTaken = value - enemyArmor;
        enemyHP -= damageTaken;
        healthBar.UpdateHealthBar(enemyHP, enemyMaxHP);
        damagePopUp.GetComponent<DamagePopUp>().Create(healthBar.transform, value, crit) ;
        print(enemyHP);

    }


}
