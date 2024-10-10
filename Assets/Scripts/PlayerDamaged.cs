using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : MonoBehaviour
{

    public PlayerInventory playerStats;
    public PlayerMovement playerMovement;



    // Start is called before the first frame update
    void Start()
    {
        playerStats=transform.parent.GetComponent<PlayerInventory>();
        playerMovement=transform.parent.GetComponent<PlayerMovement>();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !playerMovement.isDashing)
        {
            playerStats.GetDamaged(collision.gameObject.GetComponent<EnemyBehavior>().enemyDamage);
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
