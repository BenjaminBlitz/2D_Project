using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingEnemies : MonoBehaviour
{

    public PlayerInventory playerStats;
    //[SerializeField] private GameObject playerGFX;

    // Start is called before the first frame update
    void Start()
    {
       playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //************************************************** Crit
        float rand = Random.value;
        if (rand < (playerStats.playerCrit / 100))
        {
            if (gameObject.GetComponent<Collider2D>().GetType() == typeof(BoxCollider2D)
            || gameObject.GetComponent<Collider2D>().GetType() == typeof(CircleCollider2D))
            {

                if (other.gameObject.CompareTag("Enemy"))
                {

                    other.gameObject.GetComponent<EnemyBehavior>().Hitted(playerStats.playerAtk * 2,true) ;
                    other.gameObject.GetComponent<EnemyBehavior>().playerStats = playerStats;
                    //Destroy(other.gameObject);

                    if (gameObject.layer == 9)
                    {
                        Destroy(gameObject);
                    }

                    playerStats.LifeSteal(true);

                }
            }
        }
        //***************************************************** No crit
        else if (gameObject.GetComponent<Collider2D>().GetType() == typeof(BoxCollider2D)
            || gameObject.GetComponent<Collider2D>().GetType() == typeof(CircleCollider2D))
        {

            if (other.gameObject.CompareTag("Enemy"))
            {

                other.gameObject.GetComponent<EnemyBehavior>().Hitted(playerStats.playerAtk,false);
                other.gameObject.GetComponent<EnemyBehavior>().playerStats=playerStats;
                //Destroy(other.gameObject);

                if (gameObject.layer == 9)
                {
                    Destroy(gameObject);
                }
                playerStats.LifeSteal(false);
            }
        }
        
        /*
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (transform.parent?.gameObject.GetComponent<Collider2D>().GetType() == typeof(CapsuleCollider2D) ) 
            
            
            { 
                playerStats.GetDamaged(); 
            }
        }*/
    }


}
