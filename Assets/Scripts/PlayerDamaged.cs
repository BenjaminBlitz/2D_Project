using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : MonoBehaviour
{

    public PlayerInventory playerStats;



    // Start is called before the first frame update
    void Start()
    {
        playerStats=transform.parent.GetComponent<PlayerInventory>();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerStats.GetDamaged(collision.gameObject.GetComponent<EnemyBehavior>().enemyDamage);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
