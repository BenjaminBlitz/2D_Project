using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public PlayerInventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject player = other.gameObject;
        GameObject item = gameObject.transform.parent.gameObject;
        string x = item.name;
        playerInventory = player.GetComponent<PlayerInventory>();

        if (player.CompareTag("Player"))
        {
            if (x.StartsWith("SpeedItem"))
            {
                playerInventory.bootsSpeed++;
                Destroy(item);
            }
            else if (x.StartsWith("JumpItem"))
            {
                playerInventory.jumpItem++;
                Destroy(item);
            }
            else if (x.StartsWith("ASitem"))
            {
                playerInventory.atkSpeedItem++;
                Destroy(item);
            }
            else if (x.StartsWith("DefItem"))
            {
                playerInventory.defItem++;
                Destroy(item);
            }
            else if (x.StartsWith("ATKitem"))
            {
                playerInventory.atkItem++;
                Destroy(item);
            }
            else if (x.StartsWith("HPitem"))
            {
                playerInventory.HpUp(1);
                Destroy(item);
            }
            else if (x.StartsWith("CritItem"))
            {
                playerInventory.critItem++;
                Destroy(item);
            }
            else if (x.StartsWith("LifeStealItem"))
            {
                playerInventory.lifeStealItem++;
                Destroy(item);
            }
            else if (x.StartsWith("DoubleDropItem"))
            {
                playerInventory.doubleDropItem++;
                Destroy(item);
            }
            else if (x.StartsWith("JetPackItem"))
            {
                playerInventory.jetPackItem++;
                Destroy(item);
            }
            else if (x.StartsWith("DoubleHPItem"))
            {
                playerInventory.HpUp(2);
                Destroy(item);
            }
            else if (x.StartsWith("CritHealItem"))
            {
                playerInventory.critHealItem++;
                Destroy(item);
            }

        }
    }

}
