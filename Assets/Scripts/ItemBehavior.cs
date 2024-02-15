using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{

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

        if (player.CompareTag("Player"))
        {
            if (x.StartsWith("SpeedItem"))
            {
                player.GetComponent<PlayerInventory>().bootsSpeed++;
                Destroy(item);
            }
            else if (x.StartsWith("JumpItem"))
            {
                player.GetComponent<PlayerInventory>().jumpWings++;
                Destroy(item);
            }
            else if (x.StartsWith("ASitem"))
            {
                player.GetComponent<PlayerInventory>().atkSpeedItem++;
                Destroy(item);
            }


        }
    }

}
